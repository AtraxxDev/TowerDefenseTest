using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveDataSO", menuName = "Scriptable Objects/WaveDataSO")]
public class WaveDataSO : ScriptableObject
{
    public Waves[] waves;
}

[Serializable]
public struct Waves
{
    public EnemyDataSO[] enemiesType;
    public int[] enemyCountsType;    // este arreglo debera coincidir con el de los enemiesType osea que si en enemiesType[0] tengo zorros y en enemyCountsType[0] 5 , saldran 5 zorros
    public float spawnInterval;
    public string[] spawnPointNames;

}
