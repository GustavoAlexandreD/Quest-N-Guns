using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float minimumSpawnTime;

    [SerializeField]
    private float maximumSpawnTime;

    [SerializeField]
    private int maxEnemies;

    public static int currentEnemies = 0;  // contador global

    private float timeUntilSpawn;

    private void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            TrySpawnEnemy();
            SetTimeUntilSpawn();
        }
    }

    private void TrySpawnEnemy()
    {
        if (currentEnemies >= maxEnemies)
            return; // NÃO SPAWNA SE PASSOU DO LIMITE

        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        currentEnemies++;
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = UnityEngine.Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}

