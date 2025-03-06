using System.Collections;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IAttackable
{
    public TowerDataSO towerData;
    public Transform target;
    public Transform pivotProjectile;

    private Quaternion initialRotation;
    public bool isAttacking = false;

    private Coroutine attackCoroutine;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        DetectEnemies();

        RotateTowardsTarget();

        if (target != null && IsInAttackRange(target))
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(DamageLoop());
            }
        }
        else
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    public abstract void Attack();

    private IEnumerator DamageLoop()
    {
        while (target != null && IsInAttackRange(target))
        {
            Attack();
            yield return StartCoroutine(AttackCooldown()); 
        }

        attackCoroutine = null; 
    }

    public bool IsInAttackRange(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= towerData.attackRange;
    }

    private void DetectEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, towerData.detectionRange);

        Transform enemyTarget = null;
        float towerDistance = towerData.detectionRange;

        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent<IDamagable>(out IDamagable enemy) && !collider.GetComponent<BaseTower>())
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < towerDistance)
                {
                    enemyTarget = collider.transform;
                    towerDistance = distance;
                }
            }
        }

        target = enemyTarget;
    }


    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

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
