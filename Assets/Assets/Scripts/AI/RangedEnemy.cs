using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public override void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;


            GameObject projectileObj = Instantiate(projectilePrefab, shootPoint.position,shootPoint.rotation);
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetTarget(target,enemyData.damage);
            }
            Debug.Log("El ranged enemy ha atacado la BaseTower");

            StartCoroutine(AttackCooldown());
        }
    }

    public override void Die()
    {
        CoinManager.Instance.AddCoins(50);
        base.Die(); // Llama la lógica de la clase padre para destruir el objeto
    }

    private void OnDrawGizmos()
    {
        if (enemyData != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, enemyData.attackRange);
        }
    }
}
