using UnityEngine;
using System.Collections;
using TMPro; // Añade esto para el texto

public class TripleShotPowerup : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float powerupDuration = 10f;
    [SerializeField] private int cost = 1;
    [SerializeField] private ParticleSystem collectEffect;
    [SerializeField] private GameObject notEnoughTextPrefab; // Arrastra un prefab con TextMeshPro
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.playerCoins >= cost)
            {
                // Comprar power-up
                GameManager.Instance.SubtractCoins(cost); // Método para quitar monedas
                StartCoroutine(PowerupSequence(other));
            }
            else
            {
                // Mostrar mensaje de "Not enough money"
                ShowNotEnoughMoney();
            }
        }
    }

    IEnumerator PowerupSequence(Collider player)
    {
        // Instanciar efecto
        Instantiate(collectEffect, transform.position, Quaternion.identity);
        
        // Ocultar powerup
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Iniciar powerup
        BulletPool.Instance.SetTripleShot(true);
        yield return new WaitForSeconds(powerupDuration);
        BulletPool.Instance.SetTripleShot(false);

        // Desactivar completamente
        gameObject.SetActive(false);
    }

    void ShowNotEnoughMoney()
    {
        if (notEnoughTextPrefab != null)
        {
            GameObject text = Instantiate(notEnoughTextPrefab, transform.position, Quaternion.identity);
            Destroy(text, 2f); // Destruye después de 2 segundos
        }
    }
}