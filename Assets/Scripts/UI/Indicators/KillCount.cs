using UnityEngine;
using TMPro;

public class KillCount : MonoBehaviour
{
    public int kills;
    public TextMeshProUGUI killText; // Referencia al texto de la UI

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
        killText.text = "Score: " + kills;
    }

    private void Update()
    {
        AddKill();
    }
}
