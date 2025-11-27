using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image healthBar;

    public void UpdateHealthBar(HealthController healthController)
    {
        healthBar.fillAmount = healthController.RemainingHealthPercentage;
    }
}
