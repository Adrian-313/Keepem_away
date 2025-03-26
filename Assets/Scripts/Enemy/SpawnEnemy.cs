using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] SpawnEnemyPrefab; 
    public Transform[] SpawnPoints; 
    [SerializeField] private float initialSpawnRate = 5f; // Tiempo inicial entre spawns
    [SerializeField] private float minSpawnRate = 1f; // Tiempo mínimo entre spawns
    [SerializeField] private float spawnAcceleration = 0.9f; // Factor de reducción del tiempo

    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) // Bucle infinito
        {
            yield return new WaitForSeconds(currentSpawnRate);

            Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            int indexEnemy = Random.Range(0, SpawnEnemyPrefab.Length);
            Instantiate(SpawnEnemyPrefab[indexEnemy], spawnPoint.position, spawnPoint.rotation);

            // Reducimos el tiempo de spawn progresivamente hasta el mínimo
            currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate * spawnAcceleration);
        }
    }
}
