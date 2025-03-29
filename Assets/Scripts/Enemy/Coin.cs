using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1; // Valor de cada moneda
    [SerializeField] private float rotationSpeed = 100f; // Velocidad de rotación
    [SerializeField] private float attractDistance = 3f; // Distancia para atraer al jugador
    [SerializeField] private float attractSpeed = 5f; // Velocidad de atracción

    private Transform player;
    private bool canAttract = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Esperar un momento antes de poder atraer la moneda
        Invoke(nameof(EnableAttract), 0.5f);
    }

    void Update()
    {
        // Rotación constante
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

       
    }

    void EnableAttract()
    {
        canAttract = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        // Aquí puedes añadir lógica para aumentar el dinero del jugador
        GameManager.Instance.AddCoins(coinValue);
        
        // Efecto opcional (sonido/partículas)
        //Destroy(gameObject);
    }
}