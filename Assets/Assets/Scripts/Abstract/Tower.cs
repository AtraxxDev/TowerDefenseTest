using System.Collections;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IAttackable
{
    public TowerDataSO towerData;
    public Transform target;

    private Quaternion initialRotation;
    public bool isAttacking = false;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        DetectEnemies();

        RotateTowardsTarget();

        if (target != null && IsInAttackRange(target) && !isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
        else
        {
            if (isAttacking)
            {
                StopCoroutine(AttackCoroutine());
                isAttacking = false;
            }
        }
    }

    public abstract void Attack();

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        // Mientras haya un objetivo y esté dentro del rango de ataque, ataca continuamente
        while (target != null && IsInAttackRange(target))
        {
            Attack(); // Ejecuta el ataque
            yield return null; 
        }

        isAttacking = false; // Sale del modo ataque cuando el objetivo se sale del rango o muere
    }

    private bool IsInAttackRange(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= towerData.attackRange;
    }

    private void DetectEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, towerData.detectionRange);

        Transform closestTarget = null;
        float closestDistance = towerData.detectionRange;

        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent<IDamagable>(out IDamagable enemy))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestTarget = collider.transform;
                    closestDistance = distance;
                }
            }
        }

        target = closestTarget;
    }

    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * 2f);
        }
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(towerData.attackCooldown);
    }
}
