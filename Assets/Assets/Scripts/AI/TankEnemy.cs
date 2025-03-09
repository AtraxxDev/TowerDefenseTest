using System;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

public class TankEnemy : Enemy
{
    [Header("Spawn Settings")]
    public EnemyDataSO spawnedEnemy;
    public int count = 5;

    public static event Action OnEnemySpawned;


    public override void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyData.attackRange, layerAttack);

            foreach (var hitCollider in hitColliders)
            {
                BaseTower baseTower = hitCollider.GetComponent<BaseTower>();

                if (baseTower != null)
                {
                    baseTower.TakeDamage(enemyData.damage);
                    Debug.Log("El TankEnemy ha atacado la BaseTower");
                }
            }

            StartCoroutine(AttackCooldown());
        }
    }

    public override void Die()
    {
        base.Die(); // Llama la lógica de la clase padre para destruir el objeto
        CoinManager.Instance.AddCoins(20);
        SpawnEnemiesOnDeath();
    }

    private void SpawnEnemiesOnDeath()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position + UnityEngine.Random.insideUnitSphere * 2f;
            spawnPosition.y = 0f;

            Enemy newEnemy = Instantiate(spawnedEnemy.prefab, spawnPosition, spawnedEnemy.prefab.transform.rotation)
                             .GetComponent<Enemy>();

            if (newEnemy != null)
            {
                OnEnemySpawned?.Invoke();

                WaveSpawner waveSpawner = FindFirstObjectByType<WaveSpawner>();

                newEnemy.OnDie += waveSpawner.OnEnemyDie;
            }
        }

        Debug.Log($"TankEnemy ha generado {count} enemigos al morir.");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,enemyData.attackRange);
    }
}
