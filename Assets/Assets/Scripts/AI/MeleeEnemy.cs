using UnityEngine;

public class MeleeEnemy : Enemy
{
    public override void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyData.attackRange);

            foreach (var hitCollider in hitColliders)
            {
                BaseTower baseTower = hitCollider.GetComponent<BaseTower>();

                if (baseTower != null)
                {
                    baseTower.TakeDamage(enemyData.damage);
                    Debug.Log("El zorro ha atacado la BaseTower");
                }
            }

            StartCoroutine(AttackCooldown());
        }
    }

    private void OnDrawGizmos()
    {
        if (enemyData != null)
        {
            Gizmos.color = Color.yellow;  
            Gizmos.DrawWireSphere(transform.position, enemyData.attackRange); 
        }

    }

}
