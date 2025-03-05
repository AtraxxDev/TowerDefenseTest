using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour, IWave
{
    private int totalEnemiesInWave;
    private int enemiesRemaining;

    public IEnumerator StartWave(WaveDataSO data, int waveIndex, EnemySpawner enemySpawner, Transform[] spawnPoints)
    {

        // Validar índice de la oleada
        if (data == null || waveIndex < 0 || waveIndex >= data.waves.Length)
        {
            Debug.LogError("WaveSpawner: Índice de oleada inválido o WaveDataSO no asignado.");
            yield break;
        }

        Waves currentWave = data.waves[waveIndex];
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
                // Spawnear enemigo y obtener la referencia
                Enemy spawnedEnemy = enemySpawner.SpawnEnemy(currentWave.enemiesType[i], spawnPoints[j % spawnPoints.Length]);

                if (spawnedEnemy != null)
                {
                    spawnedEnemy.OnDie += OnEnemyDie; // Suscribirse al evento OnDie
                }

                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
        }

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
