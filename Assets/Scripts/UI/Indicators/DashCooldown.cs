using UnityEngine;
using UnityEngine.UI;

public class DashCooldown : MonoBehaviour
{
    public Image imageCooldown;
    private bool isCooldown = false;
    private float cooldownTime = 5f;
    private float cooldownTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageCooldown.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UseDash();
        }


        if(isCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer < 0f)
        {
            isCooldown = false;
            imageCooldown.fillAmount = 0f;
        }

        else
        {
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
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
            cooldownTimer = cooldownTime;
        }
    }
}
