using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour, IWave
{
    private int totalEnemiesInWave;
    private int enemiesRemaining;
    private bool waveInProgress = false;
    public float spawnDelay;

    public IEnumerator StartWave(WaveDataSO data, int waveIndex, EnemySpawner enemySpawner, Transform[] spawnPoints)
    {
        if (waveInProgress) yield break; // Evita llamadas duplicadas
        waveInProgress = true;

        // Validar índice de la oleada
        if (data == null || waveIndex < 0 || waveIndex >= data.waves.Length)
        {
            Debug.LogError("WaveSpawner: Índice de oleada inválido o WaveDataSO no asignado.");
            yield break;
        }

        Waves currentWave = data.waves[waveIndex]; // Obtener la oleada actual
        totalEnemiesInWave = 0;

        foreach (int count in currentWave.enemyCountsType)
        {
            totalEnemiesInWave += count;
        }
        enemiesRemaining = totalEnemiesInWave;

        // Spawn enemigo por enemigo con un delay
        for (int i = 0; i < currentWave.enemiesType.Length; i++)
        {
            for (int j = 0; j < currentWave.enemyCountsType[i]; j++)
            {
                enemySpawner.SpawnEnemy(currentWave.enemiesType[i], spawnPoints[j % spawnPoints.Length]);
                yield return new WaitForSeconds(currentWave.spawnInterval); // Usar spawnInterval de la oleada
            }
        }

        waveInProgress = false;
    }

    public bool IsCompletedWave()
    {
        return enemiesRemaining == 0;
    }

    public void OnEnemyDie()
    {
        enemiesRemaining--;
        Debug.Log("Enemigos restantes: " + enemiesRemaining);
    }
}
