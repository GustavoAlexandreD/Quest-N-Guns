using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float randomMoveChangeInterval = 2f;

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
        if (playerAwarenessController.AwareOfPlayer)
        {
            targetDirection = playerAwarenessController.DirectionToPlayer.normalized;
            return;
        }

        if (Time.time >= nextRandomDirectionTime)
        {
            targetDirection = UnityEngine.Random.insideUnitCircle.normalized;
            nextRandomDirectionTime = Time.time + randomMoveChangeInterval;
        }
    }

    private void SetVelocity()
    {
        rigidBody.linearVelocity = targetDirection * speed;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}

