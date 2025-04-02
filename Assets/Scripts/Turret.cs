using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private float health = 100f;
    [SerializeField] private int cost = 1;
    public MeshRenderer turretMesh;

    void Start()
    {
        turretMesh = GetComponent<MeshRenderer>();
        turretMesh.enabled = false;


        //gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.playerCoins >= cost && turretMesh.enabled == false)
        {
            DeployTurret();
        }
    }

    public void DeployTurret()
    {   
        //gameObject.SetActive (true);
        turretMesh.enabled = true;
        GameManager.Instance.SubtractCoins(cost);
    }
}
