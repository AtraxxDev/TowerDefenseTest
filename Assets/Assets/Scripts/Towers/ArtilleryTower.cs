using UnityEngine;
using System.Collections;

public class ArtilleryTower : Tower
{
    [SerializeField] private Transform pivotProjectile;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int shotsBeforeReload = 5;
    [SerializeField] private float reloadTime = 2f; 

    private Coroutine attackCoroutine;

    public override void Attack()
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(FireProjectiles());
        }
    }

    private IEnumerator FireProjectiles()
    {
        while (target != null && IsInAttackRange(target))
        {
            for (int i = 0; i < shotsBeforeReload; i++)
            {
                ShootProjectile();
                yield return StartCoroutine(AttackCooldown());
            }

            Debug.Log("Recargando...");
            yield return new WaitForSeconds(reloadTime); 
        }

        attackCoroutine = null; 
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && target != null)
        {
            GameObject gameObjProjectile = Instantiate(projectilePrefab, pivotProjectile.position, pivotProjectile.rotation);
            Projectile projectile = gameObjProjectile.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.SetTarget(target, towerData.damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (towerData == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, towerData.detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerData.attackRange);
    }
}
