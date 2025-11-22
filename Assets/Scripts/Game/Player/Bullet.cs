using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Acerta inimigo
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            return;
        }

        // Acerta parede
        if (collision.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
