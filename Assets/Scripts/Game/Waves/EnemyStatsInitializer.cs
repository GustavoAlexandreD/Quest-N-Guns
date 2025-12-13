using UnityEngine;

public class EnemyStatsInitializer : MonoBehaviour
{
    private HealthController health;
    private EnemyAttack attack;
    private EnemyMovement movement;

    [Header("Valores base")]
    [SerializeField] private float baseHealth = 10f;
    [SerializeField] private float baseDamage = 2f;
    [SerializeField] private float baseSpeed = 2f;

    private void Awake()
    {
        health = GetComponent<HealthController>();
        attack = GetComponent<EnemyAttack>();
        movement = GetComponent<EnemyMovement>();

        ApplyWaveScaling();
    }

    private void ApplyWaveScaling()
    {
        // Vida
        float finalHealth = baseHealth * EnemyWaveStats.HealthMultiplier;
        health.SetHealth(finalHealth);

        // Dano
        attack.SetDamage(baseDamage * EnemyWaveStats.DamageMultiplier);

        // Velocidade
        movement.SetSpeed(baseSpeed * EnemyWaveStats.SpeedMultiplier);
    }
}

