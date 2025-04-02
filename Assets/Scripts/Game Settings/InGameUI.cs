using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject visibleIndicators;
    public GameObject pauseMenu;

    private bool isPaused = false;

    void Start()
    {
        gameOverScreen.SetActive(false);
        visibleIndicators.SetActive(true);
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (GameManager.Instance.isGameOver)
        {
            gameOverScreen.SetActive(true);
            visibleIndicators.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        GameManager.Instance.PauseGame(); // Llama a la función del GameManager

        if (isPaused)
        {
            visibleIndicators.SetActive(false);
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            visibleIndicators.SetActive(true);
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ReloadGame()
    {
        GameManager.Instance.ReloadScene();
    }

    public void BackToMenu()
    {
        AudioManager.Instance.PlayMusic("Menu Theme");
        GameManager.Instance.ChangeScene("Carlos");
    }

    public void InGameButtonPressed()
    {
        AudioManager.Instance.PlaySFX("Button Pressed");
    }
}
