using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnSceneReloaded;

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
        isGameOver = false;
        OnSceneReloaded.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeScene(string nameScene)
    {
        isGameOver = false;
        SceneManager.LoadScene(nameScene);
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
    public void SubtractCoins(int amount)
    {
        playerCoins -= amount;
        // Opcional: Actualizar UI aquí
    }

}
