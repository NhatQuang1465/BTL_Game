using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private int price = 50;

    public void BuyGun()
    {
        if (CoinManager.Instance != null)
        {
            if (CoinManager.Instance.totalCoin >= price)
            {
                CoinManager.Instance.SpendCoin(price);

                Debug.Log("Mua thanh cong!");
            }
            else
            {
                Debug.Log("Khong du coin!");
            }
        }
    }
}