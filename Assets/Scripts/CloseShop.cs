using UnityEngine;

public class CloseShop : MonoBehaviour
{
    public GameObject shopPanel;

    public void Close()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}