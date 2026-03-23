using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private bool hasHit = false;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (collision.CompareTag("Enemy"))
        {
            hasHit = true;

            Enemy enemy = collision.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Bullet hit enemy | damage = " + damage);
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}