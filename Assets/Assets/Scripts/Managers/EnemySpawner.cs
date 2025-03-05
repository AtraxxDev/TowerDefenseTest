using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(EnemyDataSO enemyData, Transform spawnPoint)
    {
        GameObject enemy = Instantiate(enemyData.prefab, spawnPoint.position, Quaternion.identity);

        // Obtener el componente Enemy y conectar el evento OnDie al método OnEnemyDie del WaveSpawner
        //enemy.GetComponent<Enemy>().OnDie += () => WaveManager.Instance.OnEnemyDie();  // Asegúrate de que WaveManager.Instance esté correctamente referenciado
    }
}
