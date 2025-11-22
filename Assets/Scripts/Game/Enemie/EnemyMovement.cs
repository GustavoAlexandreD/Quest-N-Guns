using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float randomMoveChangeInterval = 2f; // tempo para mudar a direção aleatória

    private Rigidbody2D rigidBody;
    private PlayerAwarenessController playerAwarenessController;

    private Vector2 targetDirection;

    private float nextRandomDirectionTime;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        // --- Perseguir o player se estiver perto ---
        if (playerAwarenessController.AwareOfPlayer)
        {
            targetDirection = playerAwarenessController.DirectionToPlayer.normalized;
            return;
        }

        // --- Movimento aleatório quando o player NÃO está perto ---
        if (Time.time >= nextRandomDirectionTime)
        {
            // gera uma direção aleatória
            targetDirection = UnityEngine.Random.insideUnitCircle.normalized;

            // marca quando será gerada outra direção
            nextRandomDirectionTime = Time.time + randomMoveChangeInterval;
        }
    }

    private void SetVelocity()
    {
        rigidBody.linearVelocity = targetDirection * speed;
    }
}
