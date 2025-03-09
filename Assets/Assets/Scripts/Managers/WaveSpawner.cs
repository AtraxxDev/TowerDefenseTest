using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour, IWave
{
    private int totalEnemiesInWave;
    public int enemiesRemaining;

    private void Start()
    {
        TankEnemy.OnEnemySpawned += IncrementEnemiesRemaining;
    }

    private void OnDisable()
    {
        TankEnemy.OnEnemySpawned -= IncrementEnemiesRemaining;
    }

    private void IncrementEnemiesRemaining()
    {
        enemiesRemaining++;
        Debug.Log("Enemigo adicional generado. Enemigos restantes: " + enemiesRemaining);
    }


    public IEnumerator StartWave(WaveDataSO data, int waveIndex, EnemySpawner enemySpawner, Transform[] spawnPoints)
    {
        Waves currentWave = data.waves[waveIndex];
        totalEnemiesInWave = 0;

        foreach (int count in currentWave.enemyCountsType)
        {
            totalEnemiesInWave += count;
        }
        enemiesRemaining = totalEnemiesInWave;

        for (int i = 0; i < currentWave.enemiesType.Length; i++)
        {
            for (int j = 0; j < currentWave.enemyCountsType[i]; j++)
            {
                Enemy spawnedEnemy = enemySpawner.SpawnEnemy(currentWave.enemiesType[i], spawnPoints[j % spawnPoints.Length]);

                if (spawnedEnemy != null)
                {
                    spawnedEnemy.OnDie += OnEnemyDie;
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
