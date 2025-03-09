using System;
using UnityEngine;

public class BaseTower : MonoBehaviour, IHealth, IDamagable
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth() => currentHealth;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
        Debug.Log($"He recibido: {amount} de daño mi vida actual es de_ {currentHealth}");
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
