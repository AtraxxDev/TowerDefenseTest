using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(EnemyDataSO enemyData, Transform spawnPoint)
    {
        GameObject enemy = Instantiate(enemyData.prefab, spawnPoint.position, Quaternion.identity);

        // Obtener el componente Enemy y conectar el evento OnDie al m�todo OnEnemyDie del WaveSpawner
        //enemy.GetComponent<Enemy>().OnDie += () => WaveManager.Instance.OnEnemyDie();  // Aseg�rate de que WaveManager.Instance est� correctamente referenciado
    }
}
