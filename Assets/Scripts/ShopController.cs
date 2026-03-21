using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
}