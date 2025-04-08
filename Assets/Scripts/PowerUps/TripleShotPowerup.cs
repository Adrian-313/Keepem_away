using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TripleShotPowerup : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float powerupDuration = 10f;
    [SerializeField] private float respawnTime = 10f;
    [SerializeField] private int cost = 1;
    [SerializeField] private GameObject notEnoughTextPrefab;
    [SerializeField] private ParticleSystem collectEffect;
    [SerializeField] private ParticleSystem idleEffectInstance;    

    [Header("UI del Power-up")]
    [SerializeField] private Image powerupBar; // Barra de duración
    [SerializeField] private TextMeshProUGUI powerupText; // Texto de duración
    public GameObject textCost;
    private GameObject instantiateTextCost;

    private Renderer powerupRenderer;
    private Collider powerupCollider;

    void Start()
    {
        powerupRenderer = GetComponent<Renderer>();
        powerupCollider = GetComponent<Collider>();

        if (powerupBar != null) powerupBar.gameObject.SetActive(false);
        if (powerupText != null) powerupText.gameObject.SetActive(false);

        if(textCost != null)
        {
            instantiateTextCost = Instantiate(textCost, transform.position + Vector3.up, Quaternion.identity);
        }
        
    // Mostrar efecto si el objeto está disponible
        if (collectEffect != null)
        {
            idleEffectInstance = Instantiate(collectEffect, transform.position, Quaternion.identity, transform);
            idleEffectInstance.Play();
        }
    }

    void Update(){
        transform.Rotate(Vector3.up, 60f * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.playerCoins >= cost)
            {
                AudioManager.Instance.PlaySFX("Powerup");
                GameManager.Instance.SubtractCoins(cost);
                StartCoroutine(PowerupSequence());
            }
            else
            {
                ShowNotEnoughMoney();
            }
        }
    }

    IEnumerator PowerupSequence()
    {
        // Ocultar power-up
        powerupRenderer.enabled = false;
        powerupCollider.enabled = false;

        if (powerupRenderer.enabled == false)
        {
            idleEffectInstance.Stop();
            Destroy(idleEffectInstance.gameObject);
            Destroy(instantiateTextCost.gameObject);
        }

        // Activar UI
        if (powerupBar != null) powerupBar.gameObject.SetActive(true);
        if (powerupText != null) powerupText.gameObject.SetActive(true);

        // Restaurar opacidad completa
        SetAlpha(1f);

        // Activar triple disparo
        BulletPool.Instance.SetTripleShot(true);

        float timer = powerupDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float progress = timer / powerupDuration;

            // Actualizar UI
            if (powerupBar != null) powerupBar.fillAmount = progress;
            if (powerupText != null) powerupText.text = $"{timer:F1}s"; // Muestra con 1 decimal

            // Aplicar efecto de desvanecimiento
            SetAlpha(progress);

            yield return null;
        }

        // Desactivar triple disparo
        BulletPool.Instance.SetTripleShot(false);

        // Ocultar UI
        if (powerupBar != null) powerupBar.gameObject.SetActive(false);
        if (powerupText != null) powerupText.gameObject.SetActive(false);

        // Esperar antes de reaparecer
        yield return new WaitForSeconds(respawnTime);

        // Reaparecer power-up
        powerupRenderer.enabled = true;
        powerupCollider.enabled = true;

        // Volver a activar el efecto cuando reaparece
        if(powerupRenderer.enabled == true)
        {
            idleEffectInstance = Instantiate(collectEffect, transform.position, Quaternion.identity, transform);
            idleEffectInstance.Play();
            instantiateTextCost = Instantiate(textCost, transform.position + Vector3.up, Quaternion.identity);
        }
        
    }

    void SetAlpha(float alpha)
    {
        // Ajustar opacidad de la barra
        if (powerupBar != null)
        {
            Color barColor = powerupBar.color;
            barColor.a = alpha;
            powerupBar.color = barColor;
        }

        // Ajustar opacidad del texto
        if (powerupText != null)
        {
            Color textColor = powerupText.color;
            textColor.a = alpha;
            powerupText.color = textColor;
        }
    }

    void ShowNotEnoughMoney()
    {
        if (notEnoughTextPrefab != null)
        {
            GameObject text = Instantiate(notEnoughTextPrefab, transform.position + Vector3.up + Vector3.up , Quaternion.identity);
            Destroy(text, 2f);
        }
    }
}
