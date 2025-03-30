using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    public int coins = 0;
    public TextMeshProUGUI coinText; // Referencia al texto de la UI

    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin()
    {
        coins++;
        UpdateCoinUI();
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
    }
}
