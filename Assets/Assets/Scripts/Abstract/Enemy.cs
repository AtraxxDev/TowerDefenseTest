using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public abstract class Enemy : MonoBehaviour, IHealth
{

    public EnemyDataSO enemyData;
    public Transform target;
    public bool isAttacking = false;

    private float currentHealth;
    private NavMeshAgent agent;

    public event Action OnDie;

    public virtual void Start()
    {
        currentHealth = enemyData.maxHealth;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyData.speed;
        agent.stoppingDistance = enemyData.attackRange;

        target = FindFirstObjectByType<BaseTower>().transform;


    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            if (agent.remainingDistance <= agent.stoppingDistance && !isAttacking)
            {
                Attack();
            }
        }
    }




    public float GetCurrentHealth() => currentHealth;


    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, enemyData.maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    [ContextMenu("Die")]
    public virtual void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
    }

    public abstract void Attack();

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(enemyData.attackCooldown);
        isAttacking = false;
    }

    

}
