using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    public Transform[] SpawnPoints;
    [SerializeField] private float initialSpawnRate = 5f;
    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float spawnAcceleration = 0.9f;
    private PlayerController playerController;
    private float currentSpawnRate;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("No se encontró el PlayerController en la escena.");
            return; // Detiene la ejecución si no hay un jugador
        }

        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (playerController != null && playerController.playerHealth > 0)
        {
            yield return new WaitForSeconds(currentSpawnRate);

            if (playerController.playerHealth <= 0)
            {
                Debug.Log("El jugador ha muerto. Se detiene el spawn.");
                yield break; // Sale de la corrutina y detiene el spawn
            }

            Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            int enemyType = Random.Range(0, EnemyPool.Instance.enemyPrefabs.Length);
            GameObject enemy = EnemyPool.Instance.UseEnemy(enemyType);

            if (enemy != null)
            {
                enemy.transform.position = spawnPoint.position;
                enemy.transform.rotation = spawnPoint.rotation;
            }

            currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate * spawnAcceleration);
        }
    }
}
