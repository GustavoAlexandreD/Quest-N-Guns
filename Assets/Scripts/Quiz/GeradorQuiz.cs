using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GeradorQuiz : MonoBehaviour
{
    [Header("Banco de dados completo")]
    [Tooltip("Arraste todas as suas questões aqui")]
    [SerializeField] private List<DadosDaQuestao> todasAsQuestoes;

    public List<DadosDaQuestao> questoesDaPartida {get; private set; } = new List<DadosDaQuestao>();
    void Start()
    {
        GerarQuiz();
    }

    void GerarQuiz()
    {
        List<string> temasEscolhidos = TemaManager.Instance.GetMateriasAtivas();

        if (temasEscolhidos.Count == 0)
        {
            Debug.LogWarning("Nnehum item foi selecionado, selecionado todos como padrão...");
            temasEscolhidos = new List<string> { "Matemática", "História", "Geografia", "Inglês", "Biologia", "Física", "Química", "Sociologia", "Filosofia" };
        }

        var poolFiltrado = todasAsQuestoes.Where(q => temasEscolhidos.Contains(q.tema)).ToList();
        if(poolFiltrado.Count == 0)
        {
            Debug.LogError("Não foram encontradas questões para os temas selecionados!");
            return;
        }

        questoesDaPartida.Clear();

        AdicionarAleatorias(poolFiltrado, Dificuldade.Facil, 2);
        AdicionarAleatorias(poolFiltrado, Dificuldade.Medio, 2);
        AdicionarAleatorias(poolFiltrado, Dificuldade.Dificil, 1);

        questoesDaPartida = questoesDaPartida.OrderBy(q => q.dificuldade).ToList();

        Debug.Log($"Quiz gerado com {questoesDaPartida.Count} questões baseadas em {temasEscolhidos.Count} temas.");
    }

    private void AdicionarAleatorias(List<DadosDaQuestao> fonte, Dificuldade dif, int quantidade)
    {
        var candidatos = fonte.Where(q => q.dificuldade == dif).ToList();
        
        // Embaralha e pega a quantidade necessária
        var selecionadas = candidatos.OrderBy(x => Random.value).Take(quantidade).ToList();
        
        questoesDaPartida.AddRange(selecionadas);
    }
}
