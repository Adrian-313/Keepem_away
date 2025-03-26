using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject gameOverScreen;
    //public Button restartButton;
    //public Button backToMenuButton;

    //public Button resumeGameButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverScreen.gameObject.SetActive(false);

        //restartButton.onClick.AddListener(ReloadGame);
        //backToMenuButton.onClick.AddListener(BackToMenu);
        //resumeGameButton.onClick.AddListener(GamePaused);
    }

    public void ReloadGame()
    {
        GameManager.Instance.ReloadScene();
    }

    public void BackToMenu()
    {
        //Debug.Log("Volver al menú");
        GameManager.Instance.ChangeScene("Carlos");
    }

    public void GamePaused()
    {
        Debug.Log("Pausa");
        GameManager.Instance.PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            gameOverScreen.gameObject.SetActive(true);
        }
    }
}
