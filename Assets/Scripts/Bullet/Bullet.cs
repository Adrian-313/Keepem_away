using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{   
    [SerializeField] private float lifeTime = 3f; // Tiempo antes de desactivarse
    public float damage = 34;

    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterTime()); // Inicia la cuenta regresiva
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
