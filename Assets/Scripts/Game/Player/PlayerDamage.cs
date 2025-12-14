using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private float baseDamage = 20f;

    public float CurrentDamage => baseDamage;

    public void AddDamage(float amount)
    {
        baseDamage += amount;
    }
}
