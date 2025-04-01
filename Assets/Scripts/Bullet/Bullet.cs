using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{   
    [SerializeField] private float lifeTime = 3f; // Tiempo antes de desactivarse
    public float damage;
    [SerializeField] private float minDamage = 10f; // Daño mínimo
    [SerializeField] private float maxDamage = 35f; // Daño máximo

    private void OnEnable()
    {
        damage = Mathf.Round(Random.Range(minDamage, maxDamage));
        StartCoroutine(DeactivateAfterTime()); // Inicia la cuenta regresiva
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (BulletPool.Instance != null)
        {
            gameObject.SetActive(false);
        }
    }
}
