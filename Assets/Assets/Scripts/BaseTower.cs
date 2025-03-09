using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseTower : MonoBehaviour, IHealth, IDamagable
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    public Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth() => currentHealth;

    public void TakeDamage(int amount)
    {
        float damagePercentage = (float)amount / maxHealth;

        currentHealth -= amount;

        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }

        Debug.Log($"Salud actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void Die()
    {
        Debug.Log("La base ha sido destruida");
        GameManager.Instance.IsGameOver();
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
