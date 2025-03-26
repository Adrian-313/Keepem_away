using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    public Transform[] SpawnPoints;
    [SerializeField] private float initialSpawnRate = 5f;
    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float spawnAcceleration = 0.9f;

    private float currentSpawnRate;

    private void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnRate);

            Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            int enemyType = Random.Range(0, EnemyPool.Instance.enemyPrefabs.Length); // Tipo de enemigo aleatorio
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
