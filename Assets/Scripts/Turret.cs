using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private float health = 100f;
    [SerializeField] private int cost = 1;
    [SerializeField] private float turretCooldown;
    [SerializeField] private float turretDuration;
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
        Invoke(nameof(DisableTurret), turretDuration);
    }

    private void DisableTurret()
    {
        turretMesh.enabled = false;
        Invoke(nameof(EnableTurretAgain), turretCooldown);
    }

    private void EnableTurretAgain()
    {
        canDeploy = true;
    }
}
