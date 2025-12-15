using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;

    [SerializeField] private Vector2 minSpawnPosition;
    [SerializeField] private Vector2 maxSpawnPosition;

    public int maxEnemies;
    public static int currentEnemies = 0;

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
            return;

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
        );

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        currentEnemies++;
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    public void ForceSpawnInitialEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }
}
