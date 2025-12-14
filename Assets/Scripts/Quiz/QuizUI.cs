using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public static QuizUI Instance;

    public HealthController playerHealth;
    public PlayerMovement playerMovement;
    public PlayerDamage playerDamage;

    [Header("Conexões")]
    public GameObject painelQuiz; // Arraste o painel da UI aqui
    public GeradorQuiz geradorDePerguntas; // Arraste seu script QuizGenerator aqui

    // UI Elementos (Arraste seus textos e botões)
    public TextMeshProUGUI textoEnunciado;
    public TextMeshProUGUI textoFeedback;
    public Button[] botoesResposta; 
    public Button botaoFechar;

    private DadosDaQuestao questaoAtual;

    void Awake()
    {
        Instance = this;
        painelQuiz.SetActive(false); // Começa escondido
    }

    void Start()
    {
        // Garante que o botão de fechar chame a função certa
        botaoFechar.onClick.AddListener(FecharPainel);
    }

    public void ExibirProximaQuestao()
    {
        // Pede ao gerador (que já filtrou por tema) a próxima questão
        if(geradorDePerguntas.questoesDaPartida.Count > 0)
        {
            // Pega a primeira da fila
            questaoAtual = geradorDePerguntas.questoesDaPartida[0];
            geradorDePerguntas.questoesDaPartida.RemoveAt(0); // Remove para não repetir

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
        Time.timeScale = 0; // ⏸ PAUSA O JOGO (física e movimento param)

        textoEnunciado.text = q.enunciado;
        textoFeedback.text = ""; // Limpa feedback anterior
        botaoFechar.gameObject.SetActive(false); // Esconde botão de fechar (obriga a responder)

        // --- LÓGICA DE PREENCHIMENTO DOS BOTÕES ---
        for (int i = 0; i < botoesResposta.Length; i++)
        {
            // Reseta estado visual do botão (pode ter ficado vermelho/verde da anterior)
            botoesResposta[i].interactable = true; 
            botoesResposta[i].image.color = Color.white; 

            // Verifica se existe texto para esse botão (caso a questão tenha menos alternativas que botões)
            if (i < q.alternativas.Length)
            {
                botoesResposta[i].gameObject.SetActive(true);
                
                // Pega o componente de texto dentro do botão e muda
                botoesResposta[i].GetComponentInChildren<TextMeshProUGUI>().text = q.alternativas[i];

                // TRUQUE DO C#: Precisamos copiar o índice para usar dentro do listener
                int indiceDoBotao = i;

                // Remove cliques antigos (para não acumular) e adiciona o novo
                botoesResposta[i].onClick.RemoveAllListeners();
                botoesResposta[i].onClick.AddListener(() => AoClicarResposta(indiceDoBotao));
            }
            else
            {
                // Se não tem alternativa (ex: botão 5, mas só tem 4 respostas), esconde o botão
                botoesResposta[i].gameObject.SetActive(false);
            }
        }
    }

    // --- LÓGICA DE VERIFICAÇÃO ---
    void AoClicarResposta(int indiceEscolhido)
    {
        // 1. Bloqueia todos os botões para o jogador não clicar mais
        foreach (var btn in botoesResposta)
        {
            btn.interactable = false;
        }

        // 2. Verifica se acertou
        if (indiceEscolhido == questaoAtual.indiceCorreto)
        {
            textoFeedback.text = "Resposta Correta!";
            textoFeedback.color = Color.green;
            botoesResposta[indiceEscolhido].image.color = Color.green;

            // 🎁 RECOMPENSA ALEATÓRIA
            int recompensa = Random.Range(0, 3);

            switch (recompensa)
            {
                case 0:
                    playerHealth?.AddHealth(50f);
                    textoFeedback.text += "\n+50 Vida!";
                    break;

                case 1:
                    playerMovement?.AddSpeed(3f);
                    textoFeedback.text += "\n+3 Velocidade!";
                    break;

                case 2:
                    playerDamage?.AddDamage(2f);
                    textoFeedback.text += "\n+2 Dano!";
                    break;
            }

        }
        else
        {
            textoFeedback.text = "Incorreto!";
            textoFeedback.color = Color.red;
            botoesResposta[indiceEscolhido].image.color = Color.red; // Pinta o escolhido de vermelho
            
            // Mostra qual era o correto pintando de verde
            if (questaoAtual.indiceCorreto < botoesResposta.Length)
            {
                botoesResposta[questaoAtual.indiceCorreto].image.color = Color.green;
            }
        }

        // 3. Mostra o botão de fechar para o jogador voltar ao jogo
        botaoFechar.gameObject.SetActive(true);
    }

    // --- LÓGICA DE FECHAR ---
    void FecharPainel()
    {
        painelQuiz.SetActive(false);
        Time.timeScale = 1; // ▶ DESPAUSA O JOGO (tudo volta a se mover)
    }


}
