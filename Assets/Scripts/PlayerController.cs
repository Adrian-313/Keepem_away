using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float shootRateTime = 1f;
    [SerializeField] private float shootForce = 1000f;
    [SerializeField] private float shootRate = 0;
    public Transform SpawnBullet;
    public GameObject Bullet;
    private Rigidbody rb;
    Vector3 moveDirection;


    //Variable InputActions
    InputAction moveAction;
    InputAction shootAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Attack");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){

        OnMove();
        OnShoot();
        
        if (moveDirection != Vector3.zero)
        {
            // Rotación o direccion del personaje 
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    public void OnMove()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        moveDirection = new Vector3(moveValue.x, 0f, moveValue.y).normalized;
    }

    public void Move(){
        rb.MovePosition(transform.localPosition  + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void OnShoot()
    {
        bool shootValue = shootAction.IsInProgress();
        if (shootValue)
        {
            if (Time.time > shootRateTime)
            {
                //AudioManager.Instance.PlaySFX(audioShot);
                GameObject newBullet;

                newBullet = Instantiate(Bullet, SpawnBullet.position, SpawnBullet.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(SpawnBullet.forward * shootForce);
                shootRateTime = Time.time + shootRate;
            }
        }

    }
}
