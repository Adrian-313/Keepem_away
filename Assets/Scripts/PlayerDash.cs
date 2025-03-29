using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    
    private Rigidbody rb;
    private PlayerController playerController;
    private Vector3 dashDirection;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;

    [Header("Effects")]
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private ParticleSystem dashParticles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();

        if (dashTrail != null) dashTrail.emitting = false;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing && dashCooldownTimer <= 0f)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        // Obtener dirección de movimiento del input (WASD)
        dashDirection = playerController.GetMoveDirection();

        // Si el jugador no se está moviendo, usar la dirección hacia adelante
        if (dashDirection == Vector3.zero)
        {
            dashDirection = transform.forward;
        }

        // Normalizar dirección para evitar dash más lento en diagonales
        dashDirection.Normalize();

        // Activar efectos
        if (dashTrail != null) dashTrail.emitting = true;
        if (dashParticles != null) dashParticles.Play();

        // Desactivar control de movimiento mientras dura el dash
        playerController.SetCanMove(false);

        float startTime = Time.time;

        // Aplicar la velocidad del dash durante el tiempo indicado
        while (Time.time < startTime + dashDuration)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            yield return null;
        }

        // Restaurar control del movimiento
        isDashing = false;
        rb.linearVelocity = Vector3.zero;
        playerController.SetCanMove(true);

        // Desactivar efectos visuales
        if (dashTrail != null) dashTrail.emitting = false;
    }

    private void Update()
    {
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }
}


