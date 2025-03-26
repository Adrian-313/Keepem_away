using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    //public Button playGameButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playGameButton.onClick.AddListener(NextScene);
    }

    public void NextScene()
    {
        //Debug.Log("Cambio de escena");
        GameManager.Instance.ChangeScene("Carlos 2");
    }
}
