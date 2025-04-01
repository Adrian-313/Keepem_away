using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{
    [SerializeField] private float powerupDuration = 10f;
    [SerializeField] private ParticleSystem collectEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BulletPool.Instance.SetTripleShot(true);
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            
            Invoke(nameof(DisableTripleShot), powerupDuration);
        }
    }

    void DisableTripleShot()
    {
        BulletPool.Instance.SetTripleShot(false);
    }
}