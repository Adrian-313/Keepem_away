using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    private int damage;
    private float radius;
    private GameObject explosionEffect;
    private float speed;
    private Rigidbody rb;

    public void SetParameters(int bulletDamage, float explosionRadius, GameObject effect, float bulletSpeed)
    {
        damage = bulletDamage;
        radius = explosionRadius;
        explosionEffect = effect;
        speed = bulletSpeed;

        if (rb == null)
            rb = GetComponent<Rigidbody>();

        rb.linearVelocity = transform.forward * speed;
    }

    void OnEnable()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void OnDisable()
    {
        if (rb != null)
            rb.linearVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        Explode();
        gameObject.SetActive(false); // Devuelve al pool
    }

    void Explode()
    {
        // Efecto visual
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Daño en área
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
