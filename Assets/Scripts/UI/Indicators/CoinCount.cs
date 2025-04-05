using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    private int coins;
    public TextMeshProUGUI coinText; // Referencia al texto de la UI

    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin()
    {
        coins = GameManager.Instance.playerCoins;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        coinText.text = "x" + coins;
    }

    private void Update()
    {
        AddCoin();
    }
}
