using UnityEngine;

public class GameInit : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("PistolDefault_Bought"))
        {
            PlayerPrefs.SetInt("PistolDefault_Bought", 1);
            PlayerPrefs.SetInt("GunDamage_PistolDefault", 10);
            PlayerPrefs.Save();
        }
    }
}