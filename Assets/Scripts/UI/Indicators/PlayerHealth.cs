using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public HealthBar healthBar;
    private PlayerController playerRef;
    private Enemy enemyRef;

    private void Awake()
    {
        GameManager.Instance.OnSceneReloaded += HealthReset;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = FindAnyObjectByType<PlayerController>();
        enemyRef = FindAnyObjectByType<Enemy>();
        currentHealth = playerRef.playerHealth;
        healthBar.SetMaxHealth(playerRef.playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void UpadeHealth()
    {
        playerRef.TakeDamage(enemyRef.attackDamage);
        currentHealth = playerRef.playerHealth;
        healthBar.SetHealth(currentHealth);
    }

    private void HealthReset()
    {
        currentHealth = playerRef.playerHealth;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSceneReloaded -= HealthReset;
    }
}
