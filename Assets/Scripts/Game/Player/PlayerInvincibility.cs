using UnityEngine;

public class PlayerInvincibility : MonoBehaviour
{
    [SerializeField]
    private float invincibilityDuration;

    private InvincibilityController invincibilityController;

    private void Awake()
    {
        invincibilityController = GetComponent<InvincibilityController>();
    }

    public void startInvincibility()
    {
        invincibilityController.StartInvincibility(invincibilityDuration);
    }
}
