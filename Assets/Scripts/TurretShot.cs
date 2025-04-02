using UnityEngine;
using System.Collections;

public class TurretShot : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private int explosionDamage = 100;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float bulletSpeed = 20f;

    private float nextFireTime;
    private Transform currentTarget;
    private Turret turret;


    private void Start()
    {
        turret = GetComponent<Turret>();
    }

    void Update()
    {
        FindNearestEnemy();

        if (currentTarget != null && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FindNearestEnemy()
    {
        Collider[] allColliders = Physics.OverlapSphere(transform.position, detectionRange);
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider col in allColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = col.transform;
                }
            }
        }

        currentTarget = nearestEnemy;
    }

    void Shoot()
    {
        if (currentTarget != null && turret.turretMesh.enabled == true)
        {
            // Rotación hacia el enemigo
            Vector3 lookPos = currentTarget.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);

            // Obtener bala del pool
            GameObject bullet = BulletPool.Instance.useBullet();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            // Configurar bala
            ExplosiveBullet explosiveBullet = bullet.GetComponent<ExplosiveBullet>();
            if (explosiveBullet == null)
            {
                explosiveBullet = bullet.AddComponent<ExplosiveBullet>();
            }

            explosiveBullet.SetParameters(explosionDamage, explosionRadius, explosionEffect, bulletSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
