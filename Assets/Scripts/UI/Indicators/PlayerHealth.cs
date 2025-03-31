using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public HealthBar healthBar;

    private void Awake()
    {
        GameManager.Instance.OnSceneReloaded += HealthReset;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }

        if (currentHealth == 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void HealthReset()
    {
        currentHealth = maxHealth;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSceneReloaded -= HealthReset;
    }
}
