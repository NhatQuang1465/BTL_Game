using UnityEngine;

public class PlayerGunLoader : MonoBehaviour
{
    [SerializeField] private GameObject defaultGunObject;
    [SerializeField] private GameObject gunLv1Object;

    private void Start()
    {
        string currentGun = PlayerPrefs.GetString("CurrentGun", "DefaultGun");

        if (defaultGunObject != null)
            defaultGunObject.SetActive(currentGun == "DefaultGun");

        if (gunLv1Object != null)
            gunLv1Object.SetActive(currentGun == "GunLv1");
    }
}