using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int coinInRun = 0;
    public int totalCoin = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadTotalCoin();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoinInRun(int amount)
    {
        coinInRun += amount;
        Debug.Log("Coin in run = " + coinInRun);
    }

    public void SaveRunCoinToTotal()
    {
        Debug.Log("Saving coin. coinInRun = " + coinInRun);

        PlayerPrefs.SetInt("LastRunCoin", coinInRun);

        totalCoin += coinInRun;
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
        PlayerPrefs.Save();

        Debug.Log("Saved TotalCoin = " + totalCoin);

        coinInRun = 0;
    }

    public void LoadTotalCoin()
    {
        totalCoin = PlayerPrefs.GetInt("TotalCoin", 0);
        Debug.Log("Loaded TotalCoin = " + totalCoin);
    }

    public bool SpendCoin(int amount)
    {
        if (totalCoin >= amount)
        {
            totalCoin -= amount;
            PlayerPrefs.SetInt("TotalCoin", totalCoin);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }
}