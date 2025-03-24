using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private bool useCameraRelativeDash = true;
    
    private Rigidbody rb;
    private PlayerController playerController;
    private Vector3 moveDirection;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    private Camera mainCamera;

    // Efectos visuales (opcional)
    [Header("Effects")]
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private ParticleSystem dashParticles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        mainCamera = Camera.main;
        
        if (dashTrail != null) dashTrail.emitting = false;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing && dashCooldownTimer <= 0f)
        {
            StartCoroutine(PerformDash());
        }
    }

    private System.Collections.IEnumerator PerformDash()
    {
        // Obtener la dirección actual del movimiento
        moveDirection = playerController.GetMoveDirection();
        
        // Si no hay input de movimiento, dash hacia adelante
        if (moveDirection == Vector3.zero)
        {
            moveDirection = transform.forward;
        }
        
        // Opcional: hacer el dash relativo a la cámara
        if (useCameraRelativeDash && mainCamera != null)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            moveDirection = Quaternion.LookRotation(cameraForward) * moveDirection;
        }

        // Iniciar dash
        isDashing = true;
        dashCooldownTimer = dashCooldown;
        
        // Activar efectos
        if (dashTrail != null) dashTrail.emitting = true;
        if (dashParticles != null) dashParticles.Play();
        
        // Guardar velocidad original y desactivar control de movimiento
        float originalSpeed = playerController.GetMoveSpeed();
        playerController.SetCanMove(false);

        float startTime = Time.time;
        
        // Ejecutar dash
        while (Time.time < startTime + dashDuration)
        {
            rb.linearVelocity = moveDirection * dashSpeed;
            yield return null;
        }
        
        // Finalizar dash
        isDashing = false;
        rb.linearVelocity = moveDirection * originalSpeed;
        playerController.SetCanMove(true);
        
        // Desactivar efectos
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