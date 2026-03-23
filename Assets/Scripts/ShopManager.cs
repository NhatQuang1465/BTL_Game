using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Coin UI")]
    public TMP_Text coinText;

    [Header("Weapon Cards")]
    public WeaponCardUI[] weaponCards;

    public int CurrentCoin { get; private set; }

    private List<WeaponData> weapons = new List<WeaponData>();

    private void Start()
    {
        LoadData();
        SetupCards();
        RefreshAllUI();
    }

    void LoadData()
    {
        CurrentCoin = PlayerPrefs.GetInt("TotalCoin", 0);

        int equippedIndex = PlayerPrefs.GetInt("equipped_weapon", 0);

        weapons = new List<WeaponData>()
        {
            new WeaponData
            {
                weaponId = "pistol",
                weaponName = "Pistol Default",
                price = 0,
                damage = 10,
                isUnlocked = PlayerPrefs.GetInt("weapon_pistol_unlocked", 1) == 1,
                isEquipped = equippedIndex == 0
            },
            new WeaponData
            {
                weaponId = "rifle",
                weaponName = "Rifle Lv1",
                price = 50,
                damage = 20,
                isUnlocked = PlayerPrefs.GetInt("weapon_rifle_unlocked", 0) == 1,
                isEquipped = equippedIndex == 1
            },
            new WeaponData
            {
                weaponId = "plasma",
                weaponName = "Plasma Gun",
                price = 120,
                damage = 35,
                isUnlocked = PlayerPrefs.GetInt("weapon_plasma_unlocked", 0) == 1,
                isEquipped = equippedIndex == 2
            }
        };
    }

    void SetupCards()
    {
        for (int i = 0; i < weaponCards.Length; i++)
        {
            weaponCards[i].Setup(weapons[i], this);
        }
    }

    public void RefreshAllUI()
    {
        coinText.text = "Coin: " + CurrentCoin;

        foreach (var card in weaponCards)
        {
            card.RefreshUI();
        }
    }

    public void BuyWeapon(string weaponId)
    {
        WeaponData weapon = weapons.Find(w => w.weaponId == weaponId);
        if (weapon == null) return;
        if (weapon.isUnlocked) return;
        if (CurrentCoin < weapon.price) return;

        CurrentCoin -= weapon.price;
        weapon.isUnlocked = true;

        SaveCoin();
        SaveWeaponUnlock(weapon.weaponId, true);

        RefreshAllUI();
    }

    public void EquipWeapon(string weaponId)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].isEquipped = false;
        }

        WeaponData selectedWeapon = weapons.Find(w => w.weaponId == weaponId);
        if (selectedWeapon != null)
        {
            selectedWeapon.isEquipped = true;
        }

        SaveEquippedWeapon();
        RefreshAllUI();

        Gun gun = FindObjectOfType<Gun>();
        if (gun != null)
        {
            gun.LoadEquippedWeapon();
        }
    }

    void SaveCoin()
    {
        PlayerPrefs.SetInt("TotalCoin", CurrentCoin);
        PlayerPrefs.Save();
    }

    void SaveWeaponUnlock(string weaponId, bool unlocked)
    {
        PlayerPrefs.SetInt("weapon_" + weaponId + "_unlocked", unlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    void SaveEquippedWeapon()
    {
        int equippedIndex = 0;

        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].isEquipped)
            {
                equippedIndex = i;
                break;
            }
        }

        PlayerPrefs.SetInt("equipped_weapon", equippedIndex);
        PlayerPrefs.Save();
    }
}