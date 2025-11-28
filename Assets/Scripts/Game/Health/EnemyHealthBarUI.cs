using UnityEngine;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private UnityEngine.UI.Image fill;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        // Faz a barra sempre olhar para a câmera
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }

    public void UpdateHealthBar()
    {
        fill.fillAmount = healthController.RemainingHealthPercentage;
    }
}
