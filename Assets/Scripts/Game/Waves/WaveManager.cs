using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private Transform player;
    [SerializeField] private Transform playerSpawnPoint;

    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private int baseEnemiesPerWave = 5;

    private int waveNumber = 1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        // Se os inimigos acabaram
        if (EnemySpawner.currentEnemies == 0)
        {
            NextWave();
        }
    }

    private void StartWave()
    {
        // reseta posição do player
        player.position = playerSpawnPoint.position;

        // calcula quantos inimigos essa wave terá
        enemySpawner.maxEnemies = baseEnemiesPerWave + (waveNumber - 1) * 2;

        // reseta contador
        EnemySpawner.currentEnemies = 0;

        // força o spawn inicial
        enemySpawner.ForceSpawnInitialEnemies();
    }

    private void NextWave()
    {
        waveNumber++;
        StartWave();
    }
}
