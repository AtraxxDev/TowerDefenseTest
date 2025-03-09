using TMPro;
using UnityEngine;

public class PanelCoins : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;

    private void Start()
    {
        CoinManager.Instance.OnCoinsChanged += UpdateCoinDisplay;

    }

    private void OnDisable()
    {
        CoinManager.Instance.OnCoinsChanged -= UpdateCoinDisplay;
    }

    private void UpdateCoinDisplay(int newAmount)
    {
        coinText.text = $"Coins: {newAmount}";
    }
}
