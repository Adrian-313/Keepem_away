using UnityEngine;
using UnityEngine.UI;

public class ActivePowerup : MonoBehaviour
{
    public Image[] powerupImages; // Array de im�genes para los diferentes powerups
    private bool isActive = false; // Indica si un powerup est� activo
    private float powerupDuration = 7f; // Duraci�n del powerup en segundos
    private float powerupTimer = 0f; // Temporizador del powerup
    private int activePowerupIndex = -1; // �ndice del powerup activo

    void Start()
    {
        // Asegurar que todas las im�genes est�n desactivadas al inicio
        foreach (Image img in powerupImages)
        {
            img.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Comprobar las teclas para activar cada powerup
        if (Input.GetKeyDown(KeyCode.H)) ActivatePowerup(0);
        if (Input.GetKeyDown(KeyCode.J)) ActivatePowerup(1);
        if (Input.GetKeyDown(KeyCode.K)) ActivatePowerup(2);
        if (Input.GetKeyDown(KeyCode.L)) ActivatePowerup(3);

        // Si un powerup est� activo, aplicar el efecto y gestionar el temporizador
        if (isActive)
        {
            ApplyPowerupEffect();
        }
    }

    void ApplyPowerupEffect()
    {
        // Reducir el temporizador del powerup
        powerupTimer -= Time.deltaTime;

        if (powerupTimer <= 0f)
        {
            // Cuando el tiempo se acabe, desactivar el powerup
            isActive = false;
            powerupImages[activePowerupIndex].gameObject.SetActive(false);
            activePowerupIndex = -1; // Reiniciar el �ndice
        }
        else
        {
            // Reducir la opacidad de la imagen progresivamente
            float alpha = powerupTimer / powerupDuration; // Calcula la transparencia basada en el tiempo restante
            Color color = powerupImages[activePowerupIndex].color;
            color.a = alpha;
            powerupImages[activePowerupIndex].color = color;
        }
    }

    public void ActivatePowerup(int powerupIndex)
    {
        if (isActive)
        {
            Debug.Log("Ya hay un powerup activo");
        }
        else
        {
            // Asegurar que el �ndice es v�lido
            if (powerupIndex < 0 || powerupIndex >= powerupImages.Length)
            {
                Debug.LogError("�ndice de powerup fuera de rango");
                return;
            }

            // Activar el powerup y reiniciar el temporizador
            isActive = true;
            powerupTimer = powerupDuration;
            activePowerupIndex = powerupIndex;
            powerupImages[powerupIndex].gameObject.SetActive(true);

            // Restablecer la opacidad al m�ximo
            Color color = powerupImages[powerupIndex].color;
            color.a = 1f;
            powerupImages[powerupIndex].color = color;
        }
    }
}

