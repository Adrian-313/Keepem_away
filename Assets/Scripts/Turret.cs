using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class Turret : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private float health = 100f;
    [SerializeField] private int cost = 1;
    [SerializeField] private float turretCooldown;
    [SerializeField] private float turretDuration;
    public GameObject floatingTextCannon;
    public GameObject text;
    public MeshRenderer turretMesh;
    public ParticleSystem particleCannonSummon;
    public ParticleSystem instantiateParticle;
    public GameObject forbidden;
    public GameObject instantiateForbidden;

    private bool canDeploy = true;

    void Start()
    {
        turretMesh = GetComponent<MeshRenderer>();
        turretMesh.enabled = false;
        ShowCostText();
        ShowParticle();
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
        HideCostText();
        HideParticle();
        HideForbidden();
        Invoke(nameof(DisableTurret), turretDuration);
    }

    private void DisableTurret()
    {
        turretMesh.enabled = false;
        ShowForbidden();
        Invoke(nameof(EnableTurretAgain), turretCooldown);
    }

    private void EnableTurretAgain()
    {
        canDeploy = true;
        HideForbidden();
        ShowCostText();
        ShowParticle();
    }

    private void ShowCostText()
    {
        if (floatingTextCannon != null && text == null)
        {
            text = Instantiate(floatingTextCannon, transform.position, Quaternion.identity);
        }
    }

    private void HideCostText()
    {
        if (text != null)
        {
            Destroy(text);
            text = null;
        }
    }

    private void ShowParticle(){
        if (particleCannonSummon != null){
            instantiateParticle = Instantiate(particleCannonSummon,transform.position,Quaternion.identity);

        }
    }

    private void HideParticle(){
        if (particleCannonSummon != null){
            Destroy(instantiateParticle.gameObject);
        }
    }

    private void ShowForbidden(){
        if (forbidden != null)
        {
            instantiateForbidden = Instantiate(forbidden, transform.position + Vector3.up, Quaternion.identity);
        }
    }

    private void HideForbidden(){
        if (forbidden!= null){
            Destroy(instantiateForbidden);
        }
    }
}
