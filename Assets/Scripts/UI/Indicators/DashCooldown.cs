using UnityEngine;
using UnityEngine.UI;

public class DashCooldown : MonoBehaviour
{
    public Image imageCooldown;
    private bool isCooldown = false;
    private float cooldownTimer = 0f;

    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageCooldown.fillAmount = 0f;
        playerController = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UseDash();


        if(isCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        playerController.dashCooldown -= Time.deltaTime;

        if(cooldownTimer < 0f)
        {
            isCooldown = false;
            imageCooldown.fillAmount = 0f;
        }

        else
        {
            imageCooldown.fillAmount = cooldownTimer / playerController.dashCooldown;
        }
    }

    public void UseDash()
    {
        if(isCooldown)
        {
            Debug.Log("Can't use dash while in cooldown");
        }

        else
        {
            isCooldown = true;
            cooldownTimer = playerController.dashCooldown;
        }
    }
}
