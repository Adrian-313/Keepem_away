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
    [SerializeField] private float rotationSpeed = 5f;

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
        SmoothRotate();

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


    void SmoothRotate()
    {
        if (currentTarget != null)
        {
            Vector3 lookPos = currentTarget.position - transform.position;
            lookPos.y = 0;
            
            if (lookPos != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }

    void Shoot()
    {
        if (currentTarget != null && turret.turretMesh.enabled)
        {
            // Eliminada la rotación instantánea de aquí
            
            // Obtener bala del pool
            GameObject bullet = ExplosionPooling.Instance.useBullet();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            // Configurar bala
            ExplosiveBullet explosiveBullet = bullet.GetComponent<ExplosiveBullet>();
            if (explosiveBullet == null)
            {
                explosiveBullet = bullet.AddComponent<ExplosiveBullet>();
            }

            explosiveBullet.SetParameters(explosionDamage, explosionRadius, explosionEffect, bulletSpeed);
            explosiveBullet.SetTarget(currentTarget);
            AudioManager.Instance.PlaySFX("Turret Fire");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
