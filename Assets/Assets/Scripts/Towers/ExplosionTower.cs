using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTower : Tower
{
    private HashSet<Enemy> affectedEnemies = new HashSet<Enemy>();
    private bool isExplode = false;

    public override void Attack()
    {
        if (!isExplode)
        {
            Explode();
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionRoutine());
    }

    private void Explode()
    {
        foreach (var enemy in affectedEnemies)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(towerData.damage);
            }
        }

        isExplode = true;  
        Destroy(gameObject);
    }

    private IEnumerator DetectionRoutine()
    {
        while (!isExplode)
        {
            DetectEnemiesinRange();
            yield return StartCoroutine(AttackCooldown());
        }
    }

    private void DetectEnemiesinRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, towerData.detectionRange);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<IDamagable>(out IDamagable enemy) && !hitCollider.GetComponent<BaseTower>())
            {
                Enemy enemyComponent = hitCollider.GetComponent<Enemy>();
                if (enemyComponent != null && !affectedEnemies.Contains(enemyComponent))
                {
                    affectedEnemies.Add(enemyComponent);
                    Debug.Log("Enemigo detectado: " + enemyComponent.gameObject.name);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, towerData.detectionRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, towerData.attackRange);
    }
}
