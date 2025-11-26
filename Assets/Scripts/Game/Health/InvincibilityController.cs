using System;
using System.Collections;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private HealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
    }

    public void StartInvincibility(float duration)
    {
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        healthController.IsInvincible = true;
        yield return new WaitForSeconds(duration);
        healthController.IsInvincible = false;
    }
}
