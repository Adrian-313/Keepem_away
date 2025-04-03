using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private float health = 100f;
    [SerializeField] private int cost = 1;
    public MeshRenderer turretMesh;

    private bool canDeploy = true;

    void Start()
    {
        turretMesh = GetComponent<MeshRenderer>();
        turretMesh.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.playerCoins >= cost && canDeploy)
        {
            DeployTurret();
        }
    }

    public void DeployTurret()
    {   
        turretMesh.enabled = true;
        GameManager.Instance.SubtractCoins(cost);
        canDeploy = false;
        Invoke(nameof(DisableTurret), 20f);
    }

    private void DisableTurret()
    {
        turretMesh.enabled = false;
        Invoke(nameof(EnableTurretAgain), 30f);
    }

    private void EnableTurretAgain()
    {
        canDeploy = true;
    }
}
