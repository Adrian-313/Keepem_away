using UnityEngine;
using System.Collections.Generic;

public class ExplosionPooling : MonoBehaviour
{
    private static ExplosionPooling instance;
    public static ExplosionPooling Instance { get { return instance; } }

    [SerializeField] private int poolSize = 20; // Aumentado para triple disparo
    [SerializeField] private GameObject bulletPrefab;
    private List<GameObject> bulletList;

    public bool TripleShotActive { get; private set; } = false;

    void Start()
    {
        instance = this;
        bulletList = new List<GameObject>();
        MakePool(poolSize);
    }

    void MakePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullet.transform.parent = transform;
            bulletList.Add(bullet);
        }
    }

    public void SetTripleShot(bool active)
    {
        TripleShotActive = active;
    }

    public GameObject useBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }

        // Expandir pool si es necesario
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.parent = transform;
        bulletList.Add(newBullet);
        newBullet.SetActive(true);
        return newBullet;
    }
}
