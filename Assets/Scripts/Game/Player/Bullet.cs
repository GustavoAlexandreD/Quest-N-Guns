using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damageAmount = 20f; // Dano da bala

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se acertou um inimigo
        var health = collision.GetComponent<HealthController>();
        if (health != null && collision.GetComponent<EnemyMovement>())
        {
            health.TakeDamage(damageAmount);
            Destroy(gameObject); // destrói só a bala
            return;
        }

        // Acertou parede
        if (collision.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
