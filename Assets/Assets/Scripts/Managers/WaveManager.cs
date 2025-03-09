using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public WaveDataSO levelWaveData;
    public WaveSpawner waveSpawner;
    public EnemySpawner enemySpawner;
    public Transform[] spawnPoints;

    public int currentWaveIndex = 0;

    [SerializeField] private bool waveInProgress = false;



    private void Start()
    {
        print($" Total de Oleadas de este nivel: {levelWaveData.waves.Length}");

    }


    [ContextMenu("NextWave")]
    public void StartWave()
    {
        GameManager.Instance.StartWave();
        if (!waveInProgress)
        {
            StartCoroutine(NewWave());
        }
        else
        {
            Debug.Log("No se puede iniciar una nueva oleada, hay una en progreso.");
        }
    }

    private IEnumerator NewWave()
    {
        waveInProgress = true;

        if (currentWaveIndex < levelWaveData.waves.Length)
        {
            Debug.Log("Iniciando Oleada " + (currentWaveIndex + 1));

            yield return StartCoroutine(waveSpawner.StartWave(levelWaveData, currentWaveIndex, enemySpawner, spawnPoints));

            yield return new WaitUntil(() => waveSpawner.IsCompletedWave());

            waveInProgress = false;
            Debug.Log("Oleada " + (currentWaveIndex + 1) + " completada.");
            currentWaveIndex++;
            GameManager.Instance.EndWave();

            if (waveSpawner.IsCompletedWave() && currentWaveIndex >= levelWaveData.waves.Length)
            {
                GameManager.Instance.IsWinning();

            }

            //yield return new WaitForSeconds(3f); // Pausa antes de la siguiente oleada
        }


    }
}
