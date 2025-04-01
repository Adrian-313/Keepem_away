using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public void NextScene()
    {
        //Debug.Log("Cambio de escena");
        AudioManager.Instance.PlayMusic("Game Theme");
        GameManager.Instance.ChangeScene("julian");
    }

    public void MenuButtonPressed()
    {
        AudioManager.Instance.PlaySFX("Button Pressed");
    }
}
