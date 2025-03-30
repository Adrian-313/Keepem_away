using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f; 
    [SerializeField] private float rotationSpeed = 5f; 
    [SerializeField] private float enemyHealth = 100f;
    [SerializeField] private float attackDamage= 10f;

    [SerializeField] private GameObject coinPrefab; // Asigna el prefab de moneda en el Inspector
    [SerializeField] private int coinsToDrop = 1; // Cantidad de monedas a soltar
    [SerializeField] private float coinDropForce = 3f; // Fuerza al lanzar la moneda
    public GameObject textDamage;
    private NavMeshAgent enemyNavMeshAgent;
    private Transform playerTransform;

    void Start()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player'.");
        }
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            enemyNavMeshAgent.SetDestination(playerTransform.position);
            //MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        // Dirección hacia el jugador
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        directionToPlayer.y = 0; // Evita que el enemigo gire en el eje Y

        // Rotación suave hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);

        // Moverse hacia el jugador
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = directionToPlayer * moveSpeed;
    }

    //----------Reducir vida de acuerdo al daño recibido por la bala----------//
    public void TakeDamage(float damage)
    {
        FloatingText floatingText = Instantiate(textDamage,transform.position,Quaternion.identity).GetComponent<FloatingText>();
        _ = floatingText.SendText(damage.ToString(), Color.white);
        enemyHealth -= damage;
        if(enemyHealth <= 0){
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.AddScore(1);
        DropCoins(); // Llamar a la función de soltar monedas
        gameObject.SetActive(false);
    }

    //----------Hacer daño año al jugador----------//
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("esta colisiionando");

            PlayerController playerControllerHealth = collision.gameObject.GetComponentInParent<PlayerController>();

            if(playerControllerHealth != null)
            {
                playerControllerHealth.TakeDamage(attackDamage); 

            }
        }

        if(collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if(bullet != null)
            {
                TakeDamage(bullet.damage); // Aplicar el daño específico de la bala
            }
        }
    }

    void DropCoins()
    {
        for (int i = 0; i < coinsToDrop; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            
            // Añadir fuerza aleatoria para efecto más interesante
            Vector3 randomForce = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(0.5f, 1f),
                Random.Range(-1f, 1f)
            ) * coinDropForce;
            
            coin.GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.Impulse);
        }
    }

}
