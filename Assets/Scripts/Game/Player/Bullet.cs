using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damageAmount;

    public void SetDamage(float damage)
    {
        damageAmount = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<HealthController>();
        if (health != null && collision.GetComponent<EnemyMovement>())
        {
            health.TakeDamage(damageAmount);
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
