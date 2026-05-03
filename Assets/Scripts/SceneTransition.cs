using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitionWithPersistence : MonoBehaviour
{
    public string sceneName = "NextScene"; // Название сцены, на которую нужно перейти
    public float delay = 2f; // Время задержки в секундах

    void Start()
    {
        // DontDestroyOnLoad(gameObject);
        StartCoroutine(PlayAnimationAndSwitchScene());
    }

    private IEnumerator PlayAnimationAndSwitchScene()
    {
        // Ждём указанное время
        yield return new WaitForSeconds(delay);
        // Переход на другую сцену
        SceneManager.LoadScene(sceneName);
    }
}