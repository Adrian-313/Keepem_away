using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public void NextScene()
    {
        //Debug.Log("Cambio de escena");
        GameManager.Instance.ChangeScene("julian");
    }
}
