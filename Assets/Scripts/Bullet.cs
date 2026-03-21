using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage; // 🔥 damage của đạn

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 🎯 Nếu trúng enemy
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // hủy đạn
        }
    }
}