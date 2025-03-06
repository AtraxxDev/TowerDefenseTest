using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public WaveDataSO levelWaveData;
    public WaveSpawner waveSpawner;
    public EnemySpawner enemySpawner;
    public Transform[] spawnPoints;

    private int currentWaveIndex = 0;

    [SerializeField] private bool waveInProgress = false;



    private void Start()
    {
        print($" Total de Oleadas de este nivel: {levelWaveData.waves.Length}");
        StartWave();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //waveSpawner.OnEnemyDie();
        }
    }


    [ContextMenu("NextWave")]
    public void StartWave()
    {
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

            //yield return new WaitForSeconds(3f); // Pausa antes de la siguiente oleada
        }

        else
        {
            Debug.Log("Ya no hay mas oleadas en este nivel");
        }

    }
}
