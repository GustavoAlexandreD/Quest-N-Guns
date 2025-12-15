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
        EnemyWaveStats.CurrentWave = waveNumber; // inicializa
        StartWave();
    }

    private void Update()
    {
        if (EnemySpawner.currentEnemies == 0)
        {
            NextWave();
        }
    }

    private void StartWave()
    {
        player.position = playerSpawnPoint.position;

        enemySpawner.maxEnemies = baseEnemiesPerWave + (waveNumber - 1) * 2;

        EnemySpawner.currentEnemies = 0;

        enemySpawner.ForceSpawnInitialEnemies();
    }

    private void NextWave()
    {
        waveNumber++;
        EnemyWaveStats.CurrentWave = waveNumber;  
        StartWave();
    }
}
