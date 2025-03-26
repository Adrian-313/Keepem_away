using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool instance;
    public static BulletPool Instance { get { return instance; } }

    [SerializeField] private int poolSize = 10;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<GameObject> bulletList;

    void Start()
    {
        instance = this; // Asegura que la instancia se asigne correctamente
        bulletList = new List<GameObject>(); // Asegura que la lista esté inicializada
        MakePool(poolSize);
    }


    //Creamos la piscina de acuerdo al size que hemos definido 
    void MakePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab); //Instanciamos el prefab
            bullet.SetActive(false); // Desactivamos el prefab
            bulletList.Add(bullet); // lo añadimos a la lista anteriormente creada 
            bullet.transform.parent = transform;
        }
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

        MakePool(1);
        bulletList[bulletList.Count - 1].SetActive(true);
        return bulletList[bulletList.Count - 1];

    }

}
