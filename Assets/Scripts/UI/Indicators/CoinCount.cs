using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    public int coins = 0;
    public TextMeshProUGUI coinText; // Referencia al texto de la UI
    public int coinCost = 20; // Cantidad de monedas a descontar

    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin()
    {
        coins++;
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddCoin();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SpendCoins();
        }
    }
}
