using UnityEngine;
using System.Collections;

public class ArtilleryTower : Tower
{
    [SerializeField] private GameObject projectilePrefab;

    public override void Attack()
    {
        ShootProjectile();
    }

    private IEnumerator FireProjectiles()
    {
        while (target != null && IsInAttackRange(target))
        {
            ShootProjectile();
            yield return null;
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && target != null)
        {
            GameObject gameObjprojectile = Instantiate(projectilePrefab, pivotProjectile.position,pivotProjectile.rotation);
            Projectile projectile = gameObjprojectile.GetComponent<Projectile>();
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
