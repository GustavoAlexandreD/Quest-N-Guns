using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField]
    private float lifeStealPercent = 0.2f; // 20%

    private bool ativo = false;
    private HealthController playerHealth;

    public bool Ativo => ativo;

    private void Awake()
    {
        playerHealth = GetComponent<HealthController>();
    }

    public void Ativar()
    {
        ativo = true;
    }

    public void AplicarCura(float danoCausado)
    {
        if (!ativo || playerHealth == null) return;

        float cura = danoCausado * lifeStealPercent;
        playerHealth.AddHealth(cura);
    }
}
