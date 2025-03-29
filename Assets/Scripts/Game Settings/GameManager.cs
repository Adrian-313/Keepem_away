using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isPaused;
    public bool isGameOver;
    public int playerScore = 0;
    public int playerCoins { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        isGameOver = false;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameOver = false;
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        isGameOver = false;
    }

    public void AddScore(int points)
    {
        playerScore += points;
        Debug.Log("Puntos actuales: " + playerScore);
        // Aquí podrías también actualizar la UI del score
    }
    public void AddCoins(int amount)
    {
        playerCoins += amount;
        Debug.Log($"Monedas: {playerCoins}");
        // Aquí puedes notificar a la UI para actualizar
    }
}
