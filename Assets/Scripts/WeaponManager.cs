using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [Header("Current Weapon")]
    public string currentGunId;
    public int currentDamage;

    [Header("Optional UI / Visual")]
    [SerializeField] private Image gunUIImage;
    [SerializeField] private Sprite pistolSprite;
    [SerializeField] private Sprite rifleSprite;
    [SerializeField] private Sprite plasmaSprite;

    private void Start()
    {
        LoadCurrentGun();
    }

    public void LoadCurrentGun()
    {
        currentGunId = PlayerPrefs.GetString("CurrentGun", "PistolDefault");
        currentDamage = GetGunDamage(currentGunId);

        ApplyGunVisual(currentGunId);

        Debug.Log("Current Gun = " + currentGunId + " | Damage = " + currentDamage);
    }

    private int GetGunDamage(string gunId)
    {
        switch (gunId)
        {
            case "PistolDefault":
                return PlayerPrefs.GetInt("GunDamage_PistolDefault", 10);

            case "RifleLv1":
                return PlayerPrefs.GetInt("GunDamage_RifleLv1", 20);

            case "PlasmaGun":
                return PlayerPrefs.GetInt("GunDamage_PlasmaGun", 35);

            default:
                return 10;
        }
    }

    private void ApplyGunVisual(string gunId)
    {
        if (gunUIImage == null) return;

        switch (gunId)
        {
            case "PistolDefault":
                gunUIImage.sprite = pistolSprite;
                break;

            case "RifleLv1":
                gunUIImage.sprite = rifleSprite;
                break;

            case "PlasmaGun":
                gunUIImage.sprite = plasmaSprite;
                break;
        }
    }
}