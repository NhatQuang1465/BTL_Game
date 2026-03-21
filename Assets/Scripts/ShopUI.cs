using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;

    private void OnEnable()
    {
        if (CoinManager.Instance != null)
        {
            coinText.text = "Coin: " + CoinManager.Instance.totalCoin;
        }
    }
}