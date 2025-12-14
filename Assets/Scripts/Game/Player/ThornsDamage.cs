using UnityEngine;

public class ThornsDamage : MonoBehaviour
{
    [SerializeField]
    private float thornsDamage = 50f;

    private bool ativo = false;

    public bool Ativo => ativo;

    public void Ativar()
    {
        ativo = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!ativo) return;

        HealthController health = collision.gameObject.GetComponent<HealthController>();

        if (health != null && collision.gameObject.GetComponent<EnemyMovement>())
        {
            health.TakeDamage(thornsDamage);
        }
    }
}
