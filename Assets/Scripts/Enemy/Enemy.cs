using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; 
    [SerializeField] private float rotationSpeed = 5f; 
    [SerializeField] private float enemyHealth = 100f;
    [SerializeField] private float attackDamage= 10f;
    private Transform playerTransform;

    void Start()
    {
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

    void Update()
    {
        if (playerTransform != null)
        {
            MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        // Dirección hacia el jugador
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        directionToPlayer.y = 0; // Evita que el enemigo gire en el eje Y

        // Rotación suave hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Moverse hacia el jugador
        transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
    }

    //----------Reducir vida de acuerdo al daño recibido por la bala----------//
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0){
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    //----------Hacer daño año al jugador----------//
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")){

            Debug.Log("esta colisiionando");

            PlayerController playerControllerHealth = collision.gameObject.GetComponentInParent<PlayerController>();

            if(playerControllerHealth != null)
            {
                playerControllerHealth.TakeDamage(attackDamage); 

            }
        }

        if(collision.gameObject.CompareTag("Bullet")){
            gameObject.SetActive(false);
        }
    }

}
