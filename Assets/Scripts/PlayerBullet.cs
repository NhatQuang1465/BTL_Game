using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 2f;

    private int damage;
    private bool hasHit = false;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public int GetDamage()
    {
        return damage;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
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
                Debug.Log("PlayerBullet hit | damage = " + damage);
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}