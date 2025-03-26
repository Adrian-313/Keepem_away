using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool instance;
    public static EnemyPool Instance { get { return instance; } }

    public GameObject[] enemyPrefabs; // Diferentes tipos de enemigos
    [SerializeField] private int poolSize = 10;

    private Dictionary<int, List<GameObject>> enemyPools; // Diccionario para cada tipo de enemigo

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        enemyPools = new Dictionary<int, List<GameObject>>();

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPools[i] = new List<GameObject>();
            MakePool(i, poolSize);
        }
    }

    private void MakePool(int enemyType, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[enemyType]);
            enemy.SetActive(false);
            enemy.transform.parent = transform;
            enemyPools[enemyType].Add(enemy);
        }
    }

    public GameObject UseEnemy(int enemyType)
    {
        if (!enemyPools.ContainsKey(enemyType))
        {
            Debug.LogError($"No hay pool para el enemigo {enemyType}");
            return null;
        }

        foreach (GameObject enemy in enemyPools[enemyType])
        {
            if (!enemy.activeSelf)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        // Si no hay enemigos disponibles, creamos más
        GameObject newEnemy = Instantiate(enemyPrefabs[enemyType]);
        newEnemy.SetActive(false);
        newEnemy.transform.parent = transform;
        enemyPools[enemyType].Add(newEnemy);

        newEnemy.SetActive(true);
        return newEnemy;
    }
}
