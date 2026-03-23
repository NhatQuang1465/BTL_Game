using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyGun : MonoBehaviour
{
    [Header("Gun Info")]
    [SerializeField] private string gunId;
    [SerializeField] private int gunPrice;
    [SerializeField] private int gunDamage;

    [Header("UI")]
    [SerializeField] private Button buyButton;
    [SerializeField] private TMP_Text buttonText;

    [Header("Reference")]
    [SerializeField] private ShopUI shopUI;

    private void OnEnable()
    {
        RefreshUI();
    }

    public void BuyGun()
    {
        if (CoinManager.Instance == null) return;

        if (IsBought())
        {
            RefreshUI();
            return;
        }

        bool success = CoinManager.Instance.SpendCoin(gunPrice);

        if (!success)
        {
            Debug.Log("Khong du coin de mua " + gunId);
            RefreshUI();
            return;
        }

        PlayerPrefs.SetInt(GetBoughtKey(), 1);
        PlayerPrefs.SetInt(GetDamageKey(), gunDamage);

        // Mua xong tự trang bị luôn
        PlayerPrefs.SetString("CurrentGun", gunId);

        PlayerPrefs.Save();

        Debug.Log("Mua thanh cong " + gunId + " va da trang bi");

        if (shopUI != null)
            shopUI.RefreshAll();
        else
            RefreshUI();
    }

    public void RefreshUI()
    {
        bool bought = IsBought();
        bool enoughCoin = CoinManager.Instance != null && CoinManager.Instance.totalCoin >= gunPrice;

        if (buyButton != null)
            buyButton.interactable = !bought && enoughCoin;

        if (buttonText != null)
        {
            if (bought)
                buttonText.text = "Owned";
            else if (!enoughCoin)
                buttonText.text = "No Coin";
            else
                buttonText.text = "Buy";
        }
    }

    private bool IsBought()
    {
        return PlayerPrefs.GetInt(GetBoughtKey(), 0) == 1;
    }

    private string GetBoughtKey()
    {
        return gunId + "_Bought";
    }

    private string GetDamageKey()
    {
        return "GunDamage_" + gunId;
    }
}