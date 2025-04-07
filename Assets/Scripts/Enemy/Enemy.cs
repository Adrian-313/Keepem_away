using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f; 
    [SerializeField] private float rotationSpeed = 5f; 
    [SerializeField] private float enemyHealth = 100f;
    [SerializeField] private GameObject coinPrefab; // Asigna el prefab de moneda en el Inspector
    [SerializeField] private int coinsToDrop = 1; // Cantidad de monedas a soltar
    [SerializeField] private float coinDropForce = 3f; // Fuerza al lanzar la moneda
    public GameObject textDamage;
    public float attackDamage= 10f;
    private NavMeshAgent enemyNavMeshAgent;
    private Transform playerTransform;
    private Animator enemyAnimator;
    //private Collider attackCollider;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        //attackCollider = GetComponent<BoxCollider>();
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
        }
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
        GameManager.Instance.AddScore(10);
        DropCoins(); // Llamar a la función de soltar monedas
        gameObject.SetActive(false);
    }

    //----------Hacer daño año al jugador----------//
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            enemyAnimator.SetBool("isAttacking", true);
            enemyAnimator.SetBool("isRunning", false);
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

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            enemyAnimator.SetBool("isAttacking", true);
            enemyAnimator.SetBool("isRunning", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            enemyAnimator.SetBool("isAttacking", false);
            enemyAnimator.SetBool("isRunning", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController playerControllerHealth = other.gameObject.GetComponentInParent<PlayerController>();
            AudioManager.Instance.PlaySFX("Player Damage");
            playerControllerHealth.playerAnimator.SetBool("gotHit", true);
            if(playerControllerHealth != null)
            {
                playerControllerHealth.TakeDamage(attackDamage);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController playerControllerHealth = other.gameObject.GetComponentInParent<PlayerController>();
            playerControllerHealth.playerAnimator.SetBool("gotHit", false);
        }
    }

    void DropCoins()
    {
        for (int i = 0; i < coinsToDrop; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            
            // Añadir fuerza aleatoria para efecto más interesante
            Vector3 randomForce = new Vector3(
                0f,
                Random.Range(0.5f, 0.8f),
                0f
            ) * coinDropForce;
            
            coin.GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.Impulse);
            Destroy(coin.gameObject, 5.0f);
        }
    }

}
