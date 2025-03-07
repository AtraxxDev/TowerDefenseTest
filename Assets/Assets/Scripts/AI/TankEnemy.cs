using UnityEngine;

public class TankEnemy : Enemy
{
    [Header("Spawn Settings")]
    public EnemyDataSO spawnedEnemy;
    public int count = 5;

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
        SpawnEnemiesOnDeath();
        base.Die(); // Llama la lógica de la clase padre para destruir el objeto
    }

    private void SpawnEnemiesOnDeath()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 2f;
            spawnPosition.y = 0f; // Mantiene el enemigo en el suelo

            Instantiate(spawnedEnemy.prefab, spawnPosition, spawnedEnemy.prefab.gameObject.transform.rotation);
        }

        Debug.Log($"TankEnemy ha generado {count} enemigos al morir.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,enemyData.attackRange);
    }
}
