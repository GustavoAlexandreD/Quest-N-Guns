using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private string nomeDaCenaDoJogo; 

    [SerializeField]
    private GameObject painelMenuPrincipal; 
    
    [SerializeField]
    private GameObject painelOpcoes; 

    [SerializeField] 
    private GameObject painelConfirmacaoSaida;

    [SerializeField] 
    private GameObject painelLobby;

    // Opcional: Para evitar que o usuário clique em botões enquanto o jogo carrega
    //[SerializeField]
    //private float tempoDeEsperaAntesDeCarregar = 0.5f;

   
    public void Jogar()
    {
        // Boa Prática: Iniciar uma Coroutine para carregamento assíncrono
        //StartCoroutine(CarregarCenaAsync(nomeDaCenaDoJogo));
        SceneManager.LoadScene(nomeDaCenaDoJogo);
    }

    public void AbrirOpcoes()
    {
        // Boa Prática: Checagem de nulo antes de tentar acessar o objeto
        if (painelMenuPrincipal != null && painelOpcoes != null)
        {
            painelMenuPrincipal.SetActive(false);
            painelOpcoes.SetActive(true);
        }
    }

    /// <summary>
    /// Lida com a transição do painel de Opções de volta para o Menu Principal.
    /// Chamado pelo botão "VoltarButtom".
    /// </summary>
    public void FecharOpcoes()
    {
        if (painelMenuPrincipal != null && painelOpcoes != null)
        {
            painelOpcoes.SetActive(false);
            painelMenuPrincipal.SetActive(true);
        }
    }

    public void AbrirConfirmacaoSaida()
    {
        if (painelConfirmacaoSaida != null)
        {
            // Oculta o menu principal e mostra a confirmação
            painelMenuPrincipal.SetActive(false);
            painelConfirmacaoSaida.SetActive(true);
        }
        else
        {
            Debug.LogError("O Painel de Confirmação de Saída não está configurado no MenuManager!");
        }
    }

    public void CancelarSaida()
    {
        if (painelConfirmacaoSaida != null)
        {
            // Oculta a confirmação e retorna ao menu principal
            painelConfirmacaoSaida.SetActive(false);
            painelMenuPrincipal.SetActive(true);
        }
    }

    public void SairJogo()
    {
        Debug.Log("Saindo do Jogo..."); // Boa Prática: Log para debug
        
        // Application.Quit() só funciona em builds (jogos compilados). 
        // No editor, ele é ignorado, por isso a diretiva de compilação.
        #if UNITY_EDITOR
            // UnityEditor.EditorApplication.isPlaying = false; // Alternativa moderna
            Debug.LogWarning("O Application.Quit() é ignorado no Editor da Unity.");
        #else
            Application.Quit();
        #endif
    }

    public void AbrirLobby()
    {
        // Boa Prática: Checagem de nulo antes de tentar acessar o objeto
        if (painelMenuPrincipal != null && painelLobby != null)
        {
            painelMenuPrincipal.SetActive(false);
            painelLobby.SetActive(true);
        }
    }

    public void FecharLobby()
    {
        if (painelMenuPrincipal != null && painelLobby != null)
        {
            painelLobby.SetActive(false);
            painelMenuPrincipal.SetActive(true);
        }
    }

    /// <summary>
    /// Coroutine para carregar uma cena de forma assíncrona, permitindo 
    /// que outras coisas (como uma tela de loading) aconteçam.
    /// </summary>
    /// <param name="nomeDaCena">O nome da cena a ser carregada.</param>
//     private IEnumerator CarregarCenaAsync(string nomeDaCena)
//     {
//         // Boa Prática: Adicionar um pequeno delay antes de carregar
//         // para garantir que o som do clique seja reproduzido, etc.
//         yield return new WaitForSeconds(tempoDeEsperaAntesDeCarregar);

//         // Boa Prática: Usar LoadSceneAsync para carregamento não bloqueante
//         AsyncOperation operacao = SceneManager.LoadSceneAsync(nomeDaCena);

//         // Opcional: Adicionar lógica para barra de progresso
//         while (!operacao.isDone)
//         {
//             // float progresso = Mathf.Clamp01(operacao.progress / 0.9f);
//             // Atualizar UI de barra de progresso aqui
//             yield return null;
//         }
//     }    
    // public void Jogar(){
    //     List<string> temas = TemaManager.Instance.GetTemasAtivos();

    //     // Exemplo
    //     foreach (string t in temas){
    //         Debug.Log("Tema ativo: " + t);
    //     }
    //     // Aqui você carregaria sua cena normalmente
    //     SceneManager.LoadScene(nomeDaCenaDoJogo);
    // }

}
