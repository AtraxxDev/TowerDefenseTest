using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IHealth, IAttackable,IDamagable
{

    public EnemyDataSO enemyData;
    public Transform target;
    public bool isAttacking = false;
    public LayerMask layerAttack;

    private float currentHealth;
    private NavMeshAgent agent;

    public event Action OnDie;

    public Image healthBar;

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
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance && !isAttacking)
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

    public void ApplyReduceSpeed()
    {
        agent.speed = agent.speed / 2;
    }

    public void RestoreSpeed()
    {
        agent.speed = enemyData.speed;
    }

    public void TakeDamage(int amount)
    {
        float damagePercentage = (float)amount / enemyData.maxHealth;

        currentHealth -= amount;

        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / enemyData.maxHealth;
        }



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
