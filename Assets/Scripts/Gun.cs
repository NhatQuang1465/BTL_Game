using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public static Gun instance;

    private float rotateOffset = 180f;

    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefabs;

    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;

    [SerializeField] private int maxAmmo = 24;
    public int currentAmmo;

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private AudioManager audioManager;

    // 🔥 THÊM DAMAGE
    [SerializeField] private int damage = 10;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    void Update()
    {
        RotateGun();
        Shoot();
        Reload();
    }

    // 🔫 XOAY SÚNG
    void RotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width ||
            Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);

        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    // 🔫 BẮN
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;

            GameObject bullet = Instantiate(bulletPrefabs, firePos.position, firePos.rotation);

            // 🔥 TRUYỀN DAMAGE SANG BULLET
            Bullet b = bullet.GetComponent<Bullet>();
            if (b != null)
            {
                b.SetDamage(damage);
            }

            currentAmmo--;
            UpdateAmmoText();
            audioManager.PlayShootSound();
        }
    }

    // 🔄 NẠP ĐẠN
    void Reload()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            UpdateAmmoText();
            audioManager.PlayReloadSound();
        }
    }

    // 🧾 UI ĐẠN
    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo > 0 ? currentAmmo.ToString() : "Empty";
        }
    }

    // 🔥 HÀM NÂNG CẤP DAMAGE (SHOP GỌI)
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
        Debug.Log("Damage mới: " + damage);
    }

    // (OPTION) Lấy damage nếu cần
    public int GetDamage()
    {
        return damage;
    }
}