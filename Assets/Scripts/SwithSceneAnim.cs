using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Для UI

public class SwithSceneAnim : MonoBehaviour
{
    public string sceneName = "NextScene"; // Название сцены, которую нужно загрузить
    public Animator animator; // Объект Animator
    public string triggerName = "Start"; // Имя триггера для запуска анимации
    public float delay = 2f; // Время задержки перед переключением сцены

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Этот метод вызывается при нажатии на кнопку UI
    public void OnButtonClicked()
    {
        StartCoroutine(PlayAnimationAndSwitchScene());
    }

    private IEnumerator PlayAnimationAndSwitchScene()
    {
        // Запускаем анимацию
        animator.SetTrigger(triggerName);

        // Ждём указанное время (длительность анимации)
        yield return new WaitForSeconds(delay);

        // Переключаем сцену
        SceneManager.LoadScene(sceneName);
    }
}
