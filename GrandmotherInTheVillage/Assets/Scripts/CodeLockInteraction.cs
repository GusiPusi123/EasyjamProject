// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class CodeLockInteraction : MonoBehaviour
// {
//     public GameObject codeLockUI; // UI панель с кодлоком
//     public Text codeDisplay; // текст для отображения введённых цифр
//     public GameObject otherTextObject; // объект с текстом, который нужно скрывать/показывать
//     public string sceneName = "CatSceneEnd"; // сцена для загрузки
//     public string correctCode = "1234"; // правильный код
//     public KeyCode toggleUIKey = KeyCode.E; // клавиша для открытия/закрытия UI
//     public Animator lockAnimator; // добавьте здесь компонент Animator

//     private string inputCode = "";
//     private bool isUIActive = false; // отслеживаем состояние UI

//     void Update()
//     {
//         if (Input.GetKeyDown(toggleUIKey))
//         {
//             if (isUIActive)
//             {
//                 HideCodeLock();
//             }
//             else
//             {
//                 ShowCodeLock();
//             }
//         }
//     }

//     public void ShowCodeLock()
//     {
//         codeLockUI.SetActive(true);
//         inputCode = "";
//         UpdateCodeDisplay();

//         // скрываем объект с текстом
//         if (otherTextObject != null)
//         {
//             otherTextObject.SetActive(false);
//         }

//         // останавливаем время
//         Time.timeScale = 0f;

//         // показываем курсор
//         Cursor.visible = true;
//         Cursor.lockState = CursorLockMode.None;

//         isUIActive = true;
//     }

//     public void HideCodeLock()
//     {
//         codeLockUI.SetActive(false);

//         // показываем объект с текстом
//         if (otherTextObject != null)
//         {
//             otherTextObject.SetActive(true);
//         }

//         // возобновляем время
//         Time.timeScale = 1f;

//         // скрываем курсор
//         Cursor.visible = false;
//         Cursor.lockState = CursorLockMode.Locked;

//         isUIActive = false;
//     }

//     public void OnDigitButtonPress(string digit)
//     {
//         if (inputCode.Length < correctCode.Length)
//         {
//             inputCode += digit;
//             UpdateCodeDisplay();

//             if (inputCode.Length == correctCode.Length)
//             {
//                 CheckCode();
//             }
//         }
//     }

//     private void UpdateCodeDisplay()
//     {
//         if (codeDisplay != null)
//         {
//             codeDisplay.text = inputCode;
//         }
//     }

//     private void CheckCode()
//     {
//         if (inputCode == correctCode)
//         {
//             // Запускаем анимацию открытия
//             if (lockAnimator != null)
//             {
//                 lockAnimator.SetTrigger("Start");
//                 // Запускаем корутину, которая дождется окончания анимации
//                 StartCoroutine(WaitForAnimationAndChangeScene());
//             }
//             else
//             {
//                 // Если анимации нет, просто переходим сразу
//                 SceneManager.LoadScene(sceneName);
//             }
//         }
//         else
//         {
//             inputCode = "";
//             UpdateCodeDisplay();
//         }
//     }

//     private System.Collections.IEnumerator WaitForAnimationAndChangeScene()
//     {
//         // предполагаем, что название триггера "Open" и длительность анимации известны
//         // или можно дождаться окончания анимации через AnimatorStateInfo
//         AnimatorStateInfo stateInfo = lockAnimator.GetCurrentAnimatorStateInfo(0);
//         float animationLength = stateInfo.length;

//         // ждем окончания анимации
//         yield return new WaitForSeconds(animationLength);

//         // после окончания анимации меняем сцену
//         SceneManager.LoadScene(sceneName);
//     }
// }


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CodeLockInteraction : MonoBehaviour
{
    public GameObject codeLockUI; // UI панель с кодлоком
    public Text codeDisplay; // текст для отображения введённых цифр
    public string sceneName = "CatSceneEnd";
    public string correctCode = "1234"; // правильный код
    public KeyCode toggleUIKey = KeyCode.E; // клавиша для открытия/закрытия UI
    public float interactionDistance = 3f; // дистанция для взаимодействия

    private string inputCode = "";
    private bool isUIActive = false;
    private Transform playerCamera;

    // Таймер, который запускается после правильного ввода кода
    private bool timerRunning = false;
    private float timer = 0f;

    void Start()
    {
        // Получаем камеру (например, Main Camera)
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        // Проверяем, смотрите ли вы на объект и нажата ли клавиша E
        if (IsLookingAtLock() && Input.GetKeyDown(toggleUIKey))
        {
            if (isUIActive)
            {
                HideCodeLock();
            }
            else
            {
                ShowCodeLock();
            }
        }

        // Обновляем таймер, если он запущен, независимо от timeScale
        if (timerRunning)
        {
            timer += Time.unscaledDeltaTime;
            // Здесь можете проверить, если нужно, по истечении времени что-то делать
            // например: if (timer >= 10f) { ... }
        }
    }

    bool IsLookingAtLock()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Проверяем, что луч попал в этот объект
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public void ShowCodeLock()
    {
        codeLockUI.SetActive(true);
        inputCode = "";
        UpdateCodeDisplay();

        // Отключаем приостановку времени
        // Time.timeScale = 0f; // Можно оставить, если хотите, чтобы UI был без времени (заморозить игру)
        // Для работы таймера лучше оставить timeScale = 1

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isUIActive = true;
    }

    public void HideCodeLock()
    {
        codeLockUI.SetActive(false);

        // Восстановление времени
        // Time.timeScale = 1f; // Если вы использовали его для остановки, раскомментируйте

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isUIActive = false;
    }

    public void OnDigitButtonPress(string digit)
    {
        if (inputCode.Length < correctCode.Length)
        {
            inputCode += digit;
            UpdateCodeDisplay();

            if (inputCode.Length == correctCode.Length)
            {
                CheckCode();
            }
        }
    }

    private void UpdateCodeDisplay()
    {
        if (codeDisplay != null)
        {
            codeDisplay.text = inputCode;
        }
    }

    private void CheckCode()
    {
        if (inputCode == correctCode)
        {
            // Запускаем таймер, если он еще не запущен
            if (!timerRunning)
            {
                timerRunning = true;
                timer = 0f;
            }
            // Переход на другую сцену
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            inputCode = "";
            UpdateCodeDisplay();
        }
    }
}