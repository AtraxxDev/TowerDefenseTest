using UnityEngine;

public class TridentTower : Tower
{
    public override void Attack()
    {
        Debug.Log("Trident Tower esta Atacando");
        StartCoroutine(AttackCooldown());
    }


    private void OnDrawGizmos()
    {
        if (towerData == null) return;

        //  Dibuja el rango de detección (azul)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, towerData.detectionRange);

        //  Dibuja el rango de ataque (rojo)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerData.attackRange);
    }
}
