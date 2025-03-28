using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float shootRateTime = 1f;
    [SerializeField] private float shootForce = 1000f;
    [SerializeField] private float shootRate = 0.5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField]private Transform SpawnBullet;
    public float playerHealth = 100;
    private bool canMove = true;   // Deteccioón para el dash
    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector2 lookValue;
    private bool isFiring = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        RotatePlayer();
        if (isFiring)
        {
            Shoot();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveValue = context.ReadValue<Vector2>();
        moveDirection = new Vector3(moveValue.x, 0f, moveValue.y).normalized;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookValue = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        isFiring = context.performed;
    }

    public void Move()
    {
        if(canMove)
        {
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void RotatePlayer()
{
    if (SystemInfo.deviceType == DeviceType.Handheld) // En móvil, usa joystick
    {
        if (lookValue.sqrMagnitude > 0.01f)
        {
            Vector3 lookDirection = new Vector3(lookValue.x, 0, lookValue.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    else // En PC, usa el puntero del mouse
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("MouseLayer")))
        {
            //print(hit.collider.gameObject.name);
            Vector3 lookDirection = hit.point - transform.position;
            lookDirection.y = 0; // No rotar en el eje Y
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

    public void Shoot()
    {
        if (Time.time > shootRateTime)
        {
            GameObject bullet = BulletPool.Instance.useBullet();
            bullet.transform.position = SpawnBullet.position;
            bullet.transform.rotation = SpawnBullet.rotation;

            
            if(bullet.TryGetComponent(out Rigidbody bulletRb)){

                bulletRb.linearVelocity = Vector3.zero; // Reinicia la velocidad antes de disparar
                bulletRb.angularVelocity = Vector3.zero; // Evita rotaciones no deseadas
                bulletRb.AddForce(SpawnBullet.forward * shootForce, ForceMode.Impulse);
            }
            shootRateTime = Time.time + shootRate;
        }
    }
    //--------------------- métodos públicos para el dash --------------------------//
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

    //----------Reducir vida de acuerdo al daño recibido por la bala ----------//
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if(playerHealth <= 0){
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.GameOver();
    }
} 

