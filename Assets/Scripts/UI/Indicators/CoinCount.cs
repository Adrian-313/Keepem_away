using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    private int coins;
    public TextMeshProUGUI coinText; // Referencia al texto de la UI
    public int coinCost = 20; // Cantidad de monedas a descontar

    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin()
    {
        coins = GameManager.Instance.playerCoins;
        UpdateCoinUI();
    }

    public void SpendCoins()
    {
        if (coins >= coinCost)
        {
            coins -= coinCost;
            UpdateCoinUI();
            Debug.Log("Has gastado " + coinCost + " monedas.");
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }

    void UpdateCoinUI()
    {
        coinText.text = "x" + coins;
    }

    private void Update()
    {
        AddCoin();
        SpendCoins();
    }
}
