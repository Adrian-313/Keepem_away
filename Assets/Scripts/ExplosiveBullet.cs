using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    private int damage;
    private float explosionRadius;
    private GameObject explosionEffect;
    private float speed;
    private Transform target;

    public void SetParameters(int dmg, float radius, GameObject effect, float bulletSpeed)
    {
        damage = dmg;
        explosionRadius = radius;
        explosionEffect = effect;
        speed = bulletSpeed;
    }

    public void SetTarget(Transform enemyTarget)
    {
        target = enemyTarget;
    }

    void Update()
    {
        if (target == null)
        {
            gameObject.SetActive(false); // Si el objetivo muere o desaparece
            return;
        }

        // Moverse hacia el objetivo
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // Si está cerca del objetivo, explota
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 0.5f)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hitColliders)
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

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        target = null; // Limpiar referencias al desactivarse
    }
}
