using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float shootRateTime = 1f;
    [SerializeField] private float shootForce = 1000f;
    [SerializeField] private float shootRate = 0.5f;
    [SerializeField] private float rotationSpeed = 5f; // 📌 Ajusta la velocidad de rotación
    [SerializeField] private Transform SpawnBullet;
    [SerializeField] private Transform cameraTransform; // 📌 Asigna la cámara en el Inspector
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    public float coinScore = 500f;
    public float playerHealth = 100;
    private bool canMove = true;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isFiring = false;
    private bool isDashing = false;
    private float nextDashTime = 0f;
    private Vector2 moveInput; 
    private Vector2 lookInput; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }
    }

    void Update()
    {
        RotateWithCamera(); //Rotación más fluida
        if (isFiring)
        {
            Shoot();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Guardar la entrada del jugador
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        isFiring = context.performed;
    }

    public void Move()
    {
        if (canMove)
        {
            // Obtener la dirección de la cámara
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // Aplicar la dirección del movimiento basada en la cámara
            moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }


    private void RotateWithCamera()
    {
        if (cameraTransform != null)
        {
            // 🔹 El personaje rota completamente hacia la dirección de la cámara sin depender del movimiento
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


   public void Shoot()
{
    if (Time.time > shootRateTime)
    {
        if (BulletPool.Instance.TripleShotActive)
        {
            // Disparo triple
            for (int i = -1; i <= 1; i++) // -1, 0, 1 para tres balas
            {
                GameObject bullet = BulletPool.Instance.useBullet();
                Vector3 spawnPosition = SpawnBullet.position + SpawnBullet.right * i * 0.5f; // Espaciado
                bullet.transform.position = spawnPosition;
                bullet.transform.rotation = SpawnBullet.rotation;

                if (bullet.TryGetComponent(out Rigidbody bulletRb))
                {
                    bulletRb.linearVelocity = Vector3.zero;
                    bulletRb.angularVelocity = Vector3.zero;
                    bulletRb.AddForce(SpawnBullet.forward * shootForce, ForceMode.Impulse);
                }
            }
        }
        else
        {
            // Disparo normal
            GameObject bullet = BulletPool.Instance.useBullet();
            bullet.transform.position = SpawnBullet.position;
            bullet.transform.rotation = SpawnBullet.rotation;

            if (bullet.TryGetComponent(out Rigidbody bulletRb))
            {
                bulletRb.linearVelocity = Vector3.zero;
                bulletRb.angularVelocity = Vector3.zero;
                bulletRb.AddForce(SpawnBullet.forward * shootForce, ForceMode.Impulse);
            }
        }

        shootRateTime = Time.time + shootRate;
    }
}

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextDashTime)
        {
            StartCoroutine(Dash());
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        canMove = false;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            rb.MovePosition(transform.position + moveDirection * dashSpeed * Time.fixedDeltaTime);
            yield return null;
        }

        isDashing = false;
        canMove = true;
        nextDashTime = Time.time + dashCooldown;
    }

    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.GameOver();
    }
}