using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public WaveDataSO levelWaveData;
    public WaveSpawner waveSpawner;
    public EnemySpawner enemySpawner;
    public Transform[] spawnPoints;

    private int currentWaveIndex = 0;

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        while (currentWaveIndex < levelWaveData.waves.Length)
        {
            Debug.Log("Iniciando Oleada " + (currentWaveIndex + 1));

            yield return StartCoroutine(waveSpawner.StartWave(levelWaveData, currentWaveIndex, enemySpawner, spawnPoints));

            // Esperar a que la oleada termine antes de iniciar la siguiente
            yield return new WaitUntil(() => waveSpawner.IsCompletedWave());

            Debug.Log("Oleada " + (currentWaveIndex + 1) + " completada.");
            currentWaveIndex++;

            yield return new WaitForSeconds(3f); // Pausa antes de la siguiente oleada
        }

        Debug.Log("¡Todas las oleadas completadas!");
    }
}
