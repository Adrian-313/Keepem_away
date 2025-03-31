using UnityEngine;
using TMPro;

public class KillCount : MonoBehaviour
{
    public int kills = 0;
    public TextMeshProUGUI killText; // Referencia al texto de la UI

    void Start()
    {
        UpdateKillUI();
    }

    public void AddKill()
    {
        kills++;
        UpdateKillUI();
    }

    void UpdateKillUI()
    {
        killText.text = "Kills: " + kills;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddKill();
        }
    }
}
