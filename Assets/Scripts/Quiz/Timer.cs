using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    private float initialTime;
    private bool pausado = false;
    private bool tempoAcabou = false;

    void Awake()
    {
        // ===== ÚNICA ADIÇÃO NECESSÁRIA =====
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // ==================================

        initialTime = remainingTime;
    }

    void Update()
    {
        if (pausado || tempoAcabou)
            return;

        if (remainingTime > 0)
        {
            remainingTime -= Time.unscaledDeltaTime;
        }
        else
        {
            remainingTime = 0;
            tempoAcabou = true;
            timerText.color = Color.red;
            timerText.text = "00";

            // avisa o quiz que o tempo acabou
            QuizUI.Instance?.TempoEsgotado();
            return;
        }

        int seconds = Mathf.FloorToInt(remainingTime);
        timerText.text = seconds.ToString("00");
    }

    // ===== MÉTODOS CONTROLADOS PELO QUIZ =====

    public void Pausar()
    {
        pausado = true;
    }

    public void Retomar()
    {
        pausado = false;
    }

    public void Resetar()
    {
        remainingTime = initialTime;
        tempoAcabou = false;
        pausado = false;
        timerText.color = Color.white;
    }
}
