using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    // Vida atual do personagem (setada no Inspector)
    [SerializeField]
    private float currentHealth;

    // Vida máxima que o personagem pode ter (setada no Inspector)
    [SerializeField]
    private float maximumHealth;

    // Retorna a porcentagem da vida restante (0 a 1)
    public float RemainingHealthPercentage
    {
        get
        {
            return currentHealth / maximumHealth;
        }
    }

    // Quando true, o personagem não recebe dano
    public bool IsInvincible { get; set; }

    // Evento chamado quando a vida chega a 0 (morte)
    public UnityEvent OnDied;

    // Evento chamado quando o personagem recebe dano mas não morre
    public UnityEvent OnDamage;

    // Método responsável por aplicar dano ao personagem
    public void TakeDamage(float amount)
    {
        // Se já está sem vida, ignora
        if (currentHealth == 0)
        {
            return;
        }

        // Se está invencível, ignora o dano
        if (IsInvincible)
        {
            return;
        }

        // Subtrai o dano da vida atual
        currentHealth -= amount;

        // Impede que a vida fique negativa
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Se a vida chegou a zero, dispara evento de morte
        if (currentHealth == 0)
        {
            OnDied.Invoke();
        }
        else
        {
            // Caso contrário, dispara evento de "levou dano"
            OnDamage.Invoke();
        }
    }

    // Método para curar o personagem
    public void addHealth(float amount)
    {
        // Se está morto, não pode curar
        if (currentHealth == 0)
        {
            return;
        }

        // Soma vida
        currentHealth += amount;

        // Impede que ultrapasse a vida máxima
        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
    }
}
