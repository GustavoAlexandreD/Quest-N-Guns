using UnityEngine;
using UnityEngine.UI;

public class BrightnessSliderBinder : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    private void Start()
    {
        var gb = GlobalBrightness.Instance;
        if (gb == null) return;

        // Atualiza o valor visual do slider
        slider.SetValueWithoutNotify(
            PlayerPrefs.GetInt(GlobalBrightness.PREF_KEY, 70)
        );

        // Conecta o evento
        slider.onValueChanged.AddListener(gb.OnSliderChanged);
    }

    private void OnDestroy()
    {
        var gb = GlobalBrightness.Instance;
        if (gb == null) return;

        slider.onValueChanged.RemoveListener(gb.OnSliderChanged);
    }
}
