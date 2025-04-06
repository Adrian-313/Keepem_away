using UnityEngine;
using TMPro;

public class KillCount : MonoBehaviour
{
    public int kills;
    public TextMeshProUGUI killText; // Referencia al texto de la UI
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        UpdateKillUI();
    }

    public void AddKill()
    {
        kills = GameManager.Instance.playerScore;
        UpdateKillUI();
    }

    void UpdateKillUI()
    {
        //killText.text = "Score: " + kills;
        killText.text = "Score: " + GameManager.Instance.playerScore;
        highScoreText.text = "High Score: " + GameManager.Instance.highScore;
    }

    private void Update()
    {
        AddKill();
    }
}
