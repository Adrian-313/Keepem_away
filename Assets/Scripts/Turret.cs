using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private float health = 100f;
    [SerializeField] private MeshRenderer turretMesh;
    PlayerController playerRef;

    void Start()
    {
        turretMesh = GetComponent<MeshRenderer>();
        turretMesh.enabled = false;
        playerRef = FindAnyObjectByType<PlayerController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && playerRef.coinScore >= 100f && turretMesh.enabled == false)
        {
            DeployTurret();
            Debug.Log(playerRef.coinScore);
        }
    }

    public void DeployTurret()
    {
        turretMesh.enabled = true;
        playerRef.coinScore -= 100f;
    }
}
