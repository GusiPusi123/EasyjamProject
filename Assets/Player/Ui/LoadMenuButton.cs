using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuButton : MonoBehaviour
{
    // Название сцены меню
    public string menuSceneName = "MainMenu";

    // Этот метод вызывается при нажатии кнопки
    public void OnButtonClick()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}