using UnityEngine;

public class ResetGameData : MonoBehaviour
{
    public int resetCoin = 0;

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("TotalCoin", resetCoin);

        PlayerPrefs.SetInt("PistolDefault_Bought", 1);
        PlayerPrefs.SetInt("GunDamage_PistolDefault", 10);
        PlayerPrefs.SetString("CurrentGun", "PistolDefault");

        PlayerPrefs.Save();

        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.totalCoin = resetCoin;
            CoinManager.Instance.coinInRun = 0;

            // ✅ gọi đúng cách
            CoinManager.Instance.ForceUpdateUI();
        }

        Debug.Log("RESET XONG");
    }
}