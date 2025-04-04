using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float attractDistance = 1000f;
    [SerializeField] private float attractSpeed = 10f;
    [SerializeField] private float timeBeforeAttract = 0.5f;

    [Header("Componentes")]
    [SerializeField] private Collider triggerCollider; // Asignar en el Inspector
    [SerializeField] private Collider physicsCollider; // Asignar en el Inspector

    private Transform player;
    private Rigidbody rb;
    private bool canAttract = false;

    void Start()
    {
        // Buscar al jugador de forma más segura
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
        
        rb = GetComponent<Rigidbody>();
        
        // Configuración inicial de colliders
        physicsCollider.isTrigger = false;
        triggerCollider.isTrigger = true;
        
        Invoke(nameof(EnableAttraction), timeBeforeAttract);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (canAttract && player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > attractDistance)
            {
                // Cambiamos a física kinemática durante la atracción
                rb.isKinematic = true;
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    player.position,
                    attractSpeed * Time.deltaTime
                );
            }
        }
    }

    void EnableAttraction()
    {
        canAttract = true;
        Debug.Log("Atracción activada");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.name);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Jugador detectado!");
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        GameManager.Instance.AddCoins(coinValue);
        Destroy(gameObject);
    }
}