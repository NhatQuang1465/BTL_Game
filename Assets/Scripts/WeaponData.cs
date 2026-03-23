using System;

[Serializable]
public class WeaponData
{
    public string weaponId;
    public string weaponName;
    public int price;
    public int damage;

    public bool isUnlocked;
    public bool isEquipped;
}