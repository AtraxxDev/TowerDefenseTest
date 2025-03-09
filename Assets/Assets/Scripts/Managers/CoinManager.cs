using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }


    private void Start()
    {
        AddCoins(1000);
    }

    [SerializeField] private int currentCoins;

    public event Action<int> OnCoinsChanged;

    public int PlayerCoins => currentCoins;

    public bool CanAfford(int cost)
    {
        return currentCoins >= cost;
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        OnCoinsChanged?.Invoke(currentCoins); 
    }

    public bool SpendCoins(int amount)
    {
        if (CanAfford(amount))
        {
            currentCoins -= amount;
            OnCoinsChanged?.Invoke(currentCoins);
            return true;
        }
        return false;
    }
}
