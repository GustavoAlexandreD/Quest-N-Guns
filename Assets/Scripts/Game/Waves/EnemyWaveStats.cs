using UnityEngine;

public static class EnemyWaveStats
{
    public static int CurrentWave = 1;

    // Multiplicadores que crescem por wave
    public static float HealthMultiplier => 1f + (CurrentWave - 1) * 0.25f;   // +25% de vida por wave
    public static float DamageMultiplier => 1f + (CurrentWave - 1) * 0.20f;   // +20% de dano por wave
    public static float SpeedMultiplier => 1f + (CurrentWave - 1) * 0.11f;   // +10% de velocidade por wave
}
