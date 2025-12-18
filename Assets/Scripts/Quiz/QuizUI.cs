using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public static QuizUI Instance;

    [Header("Referências do Jogador")]
    public HealthController playerHealth;
    public PlayerMovement playerMovement;
    public PlayerDamage playerDamage;
    public PlayerShoot playerShoot;
    public ThornsDamage thornsDamage;
    public Vampirism vampirism;

    private List<TipoRecompensa> recompensasDisponiveis;

    [Header("Conexões")]
    public GameObject painelQuiz;
    public GeradorQuiz geradorDePerguntas;

    [Header("UI")]
    public TextMeshProUGUI textoEnunciado;
    public TextMeshProUGUI textoFeedback;
    public Button[] botoesResposta;
    public Button botaoFechar;

    private DadosDaQuestao questaoAtual;

    // =====================================================

    void Awake()
    {
        Instance = this;
        painelQuiz.SetActive(false);

    }

    void Start()
    {
        botaoFechar.onClick.AddListener(FecharPainel);

        recompensasDisponiveis = new List<TipoRecompensa>
        {
            TipoRecompensa.Vida,
            TipoRecompensa.Velocidade,
            TipoRecompensa.Dano,
            TipoRecompensa.Cadencia,
            TipoRecompensa.Espinhos
        };
    }

    // =====================================================
    // EXIBIÇÃO DO QUIZ
    // =====================================================

    public void ExibirProximaQuestao()
    {
        if (geradorDePerguntas.questoesDaPartida.Count > 0)
        {
            questaoAtual = geradorDePerguntas.questoesDaPartida[0];
            geradorDePerguntas.questoesDaPartida.RemoveAt(0);

            MontarTela(questaoAtual);
        }
        else
        {
            Debug.Log("Todas as questões já foram respondidas!");
        }
    }

    void MontarTela(DadosDaQuestao q)
    {
        painelQuiz.SetActive(true);
        Time.timeScale = 0;

        Timer.Instance?.Resetar(); // 🔄 reinicia o tempo sempre que abre o quiz

        textoEnunciado.text = q.enunciado;
        textoFeedback.text = "";
        textoFeedback.color = Color.white;
        botaoFechar.gameObject.SetActive(false);

        for (int i = 0; i < botoesResposta.Length; i++)
        {
            botoesResposta[i].interactable = true;
            botoesResposta[i].image.color = Color.white;

            if (i < q.alternativas.Length)
            {
                botoesResposta[i].gameObject.SetActive(true);
                botoesResposta[i]
                    .GetComponentInChildren<TextMeshProUGUI>().text = q.alternativas[i];

                int indice = i;
                botoesResposta[i].onClick.RemoveAllListeners();
                botoesResposta[i].onClick.AddListener(() => AoClicarResposta(indice));
            }
            else
            {
                botoesResposta[i].gameObject.SetActive(false);
            }
        }
    }

    // =====================================================
    // RESPOSTA DO JOGADOR
    // =====================================================

    void AoClicarResposta(int indiceEscolhido)
    {
        Timer.Instance?.Pausar(); // ⏸ para o timer ao responder

        foreach (var btn in botoesResposta)
            btn.interactable = false;

        if (indiceEscolhido == questaoAtual.indiceCorreto)
        {
            textoFeedback.text = "Resposta Correta!";
            textoFeedback.color = Color.green;
            botoesResposta[indiceEscolhido].image.color = Color.green;

            TipoRecompensa recompensa =
                recompensasDisponiveis[Random.Range(0, recompensasDisponiveis.Count)];

            switch (recompensa)
            {
                case TipoRecompensa.Vida:
                    playerHealth?.IncreaseMaxHealth(50f);
                    textoFeedback.text += "\n+50 Vida Máxima!";
                    break;

                case TipoRecompensa.Velocidade:
                    playerMovement?.AddSpeed(3f);
                    textoFeedback.text += "\n+3 Velocidade!";
                    break;

                case TipoRecompensa.Dano:
                    playerDamage?.AddDamage(10f);
                    textoFeedback.text += "\n+10 Dano!";
                    break;

                case TipoRecompensa.Cadencia:
                    playerShoot?.ReduceFireRate(0.4f);
                    textoFeedback.text += "\n+Cadência de Tiro!";
                    break;

                case TipoRecompensa.Espinhos:
                    if (thornsDamage != null && !thornsDamage.Ativo)
                    {
                        thornsDamage.Ativar();
                        textoFeedback.text += "\n+Espinhos!";
                        recompensasDisponiveis.Remove(TipoRecompensa.Espinhos);
                    }
                    break;

                case TipoRecompensa.Vampirismo:
                    if (vampirism != null && !vampirism.Ativo)
                    {
                        vampirism.Ativar();
                        textoFeedback.text += "\n+Vampirismo!";
                        recompensasDisponiveis.Remove(TipoRecompensa.Vampirismo);
                    }
                    break;
            }
        }
        else
        {
            textoFeedback.text = "Incorreto!";
            textoFeedback.color = Color.red;
            botoesResposta[indiceEscolhido].image.color = Color.red;

            if (questaoAtual.indiceCorreto < botoesResposta.Length)
                botoesResposta[questaoAtual.indiceCorreto].image.color = Color.green;
        }

        botaoFechar.gameObject.SetActive(true);
    }

    // =====================================================
    // TEMPO ESGOTADO
    // =====================================================

    public void TempoEsgotado()
    {
        foreach (var btn in botoesResposta)
            btn.interactable = false;

        textoFeedback.text = "⏰ Acabou o tempo!";
        textoFeedback.color = Color.red;

        if (questaoAtual != null &&
            questaoAtual.indiceCorreto < botoesResposta.Length)
        {
            botoesResposta[questaoAtual.indiceCorreto].image.color = Color.green;
        }

        botaoFechar.gameObject.SetActive(true);
    }

    // =====================================================
    // FECHAR QUIZ
    // =====================================================

    void FecharPainel()
    {
        painelQuiz.SetActive(false);
        Time.timeScale = 1;

        Timer.Instance?.Resetar();   // prepara para o próximo quiz
    }
}
