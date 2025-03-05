
using System.Collections;
using UnityEngine;

public interface IWave
{
    IEnumerator StartWave(WaveDataSO data, int waveIndex, EnemySpawner enemySpawner, Transform[] spawnPoints);
    bool IsCompletedWave();
}
