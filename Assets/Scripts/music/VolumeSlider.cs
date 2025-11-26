using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);

        // Inicializa o slider com o volume atual
        if (MusicManager.Instance != null)
        {
            slider.value = MusicManager.Instance.GetVolume();
        }
    }

    private void OnSliderChanged(float value)
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetVolume(value);
        }
    }
}
