using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    private HealthController health;

    private void Awake()
    {
        health = GetComponent<HealthController>();
        health.OnDied.AddListener(Die);
    }

    private void Die()
    {
        EnemySpawner.currentEnemies--;
        Destroy(gameObject);
    }
}
