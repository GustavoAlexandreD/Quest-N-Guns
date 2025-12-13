using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;

    [SerializeField]
    private float maximumHealth;

    public float RemainingHealthPercentage => currentHealth / maximumHealth;

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;
    public UnityEvent OnDamage;
    public UnityEvent OnHealthChanged;

    public void TakeDamage(float amount)
    {
        if (currentHealth == 0) return;
        if (IsInvincible) return;

        currentHealth -= amount;

        OnHealthChanged.Invoke();

        if (currentHealth < 0)
            currentHealth = 0;

        if (currentHealth == 0)
            OnDied.Invoke();
        else
            OnDamage.Invoke();
    }

    public void addHealth(float amount)
    {
        if (currentHealth == 0) return;

        currentHealth += amount;

        OnHealthChanged.Invoke();

        if (currentHealth > maximumHealth)
            currentHealth = maximumHealth;
    }

    public void SetHealth(float value)
    {
        maximumHealth = value;
        currentHealth = value;
        OnHealthChanged.Invoke();
    }
}
