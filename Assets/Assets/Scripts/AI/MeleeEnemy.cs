using UnityEngine;

public class MeleeEnemy : Enemy
{
    public override void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyData.attackRange,layerAttack);

            foreach (var hitCollider in hitColliders)
            {
                BaseTower baseTower = hitCollider.GetComponent<BaseTower>();


                if (baseTower != null)
                {
                    baseTower.TakeDamage(enemyData.damage);
                    Debug.Log("El melee enemy ha atacado la BaseTower");
                }

            }

            StartCoroutine(AttackCooldown());
        }
    }

    public override void Die()
    {
        CoinManager.Instance.AddCoins(25);
        base.Die(); // Llama la lógica de la clase padre para destruir el objeto
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
