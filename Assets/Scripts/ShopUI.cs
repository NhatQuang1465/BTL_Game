using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private ShopBuyGun[] gunItems;

    private void OnEnable()
    {
        RefreshAll();
    }

    public void RefreshAll()
    {
        UpdateCoinText();

        if (gunItems != null)
        {
            for (int i = 0; i < gunItems.Length; i++)
            {
                if (gunItems[i] != null)
                    gunItems[i].RefreshUI();
            }
        }
    }

    public void UpdateCoinText()
    {
        if (coinText == null) return;

        if (CoinManager.Instance != null)
            coinText.text = "Coin: " + CoinManager.Instance.totalCoin;
        else
            coinText.text = "Coin: 0";
    }
}