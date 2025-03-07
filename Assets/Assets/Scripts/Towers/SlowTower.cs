using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : Tower
{
    private HashSet<Enemy> affectedEnemies = new HashSet<Enemy>();

    public override void Attack()
    {
        StartCoroutine(SlowDownLoop());
    }

    private IEnumerator SlowDownLoop()
    {
        while (true) 
        {
            ReduceSpeed();
            yield return StartCoroutine(AttackCooldown());
        }
    }

    private void ReduceSpeed()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, towerData.attackRange);
        HashSet<Enemy> currentEnemies = new HashSet<Enemy>();

        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                currentEnemies.Add(enemy);

                if (!affectedEnemies.Contains(enemy))
                {
                    enemy.ApplyReduceSpeed();
                    affectedEnemies.Add(enemy);
                }
            }
        }

        List<Enemy> toRestore = new List<Enemy>();
        foreach (Enemy enemy in affectedEnemies)
        {
            if (!currentEnemies.Contains(enemy)) 
            {
                toRestore.Add(enemy);
            }
        }

        foreach (Enemy enemy in toRestore)
        {
            enemy.RestoreSpeed();
            affectedEnemies.Remove(enemy);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, towerData.attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, towerData.detectionRange);
    }
}
