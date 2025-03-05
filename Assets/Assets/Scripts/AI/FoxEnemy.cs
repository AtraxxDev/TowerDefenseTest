using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FoxEnemy : Enemy
{
    public override void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyData.attackRange);

            foreach (var hitCollider in hitColliders)
            {
                // Comprobamos si el objeto tiene el componente BaseTower
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
            Gizmos.color = Color.yellow;  // Color del Gizmo
            Gizmos.DrawWireSphere(transform.position, enemyData.attackRange);  // Dibuja un círculo con el radio de ataque
        }

    }

}
