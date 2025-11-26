using UnityEngine;
using UnityEngine.UI;

public class GlobalBrightness : MonoBehaviour
{
    public static GlobalBrightness Instance;

    [Header("Overlay de brilho (Image preto)")]
    public Image brilhoOverlay;

    [Header("Configurações")]
    [Range(0, 255)]
    public int defaultAlpha = 70;   // Valor inicial

    private const string PREF_KEY = "global_brightness";

    private void Awake()
    {
        // Singleton para manter o brilho entre cenas
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InicializarBrilho();
    }

    private void InicializarBrilho()
    {
        int alphaCarregado = PlayerPrefs.GetInt(PREF_KEY, defaultAlpha);
        SetAlpha(alphaCarregado, salvar: false);
    }

    /// <summary>
    /// Atualiza o alpha do overlay de brilho.
    /// Pode ser chamado diretamente por um Slider.
    /// </summary>
    public void SetAlpha(float alpha, bool salvar = true)
    {
        if (brilhoOverlay == null)
        {
            Debug.LogError("BrilhoOverlay não foi atribuído no inspector!");
            return;
        }

        float alphaInvertido = 255f - alpha;
        // Atualiza a cor
        Color c = brilhoOverlay.color;
        c.a = alphaInvertido / 255f;
        brilhoOverlay.color = c;

        // Salva o valor
        if (salvar)
            PlayerPrefs.SetInt(PREF_KEY, (int)alpha);
    }

    /// <summary>
    /// Função para conectar ao slider diretamente no Inspector.
    /// Ex: OnValueChanged → GlobalBrightness.Instance.OnSliderChanged
    /// </summary>
    public void OnSliderChanged(float value)
    {
        SetAlpha(value);
    }
}
