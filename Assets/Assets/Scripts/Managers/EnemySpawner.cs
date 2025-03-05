using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy SpawnEnemy(EnemyDataSO enemyData, Transform spawnPoint)
    {
        GameObject enemyObject = Instantiate(enemyData.prefab, spawnPoint.position, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        return enemy;
    }
}
