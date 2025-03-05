using System;
using UnityEngine;

public class BaseTower : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    public event Action OnBaseDestroy;

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
        OnBaseDestroy?.Invoke();
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
