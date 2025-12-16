using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FimDeJogoManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject fimDeJogoCanvas;
    [SerializeField] private GameObject[] canvasParaFechar;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text waveText;

    [Header("Referências")]
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private HealthController playerHealth;

    private void Awake()
    {
        fimDeJogoCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        playerHealth.OnDied.AddListener(ShowGameOver);
    }

    private void OnDisable()
    {
        playerHealth.OnDied.RemoveListener(ShowGameOver);
    }

    private void ShowGameOver()
    {
        Time.timeScale = 0f; // pausa o jogo

        foreach (GameObject canvas in canvasParaFechar)
        {
            if(canvas != null)
                canvas.SetActive(false);
        }

        scoreText.text = $"Pontuação: {scoreController.Score}";
        waveText.text = $"Numero de ondas: {EnemyWaveStats.CurrentWave}";

        fimDeJogoCanvas.SetActive(true);
    }

    // ================== BOTÕES ==================

    public void QuitGame()
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

    public void VoltarCenaMenu(string nomeCenaMenu)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nomeCenaMenu);
    }
}
