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
    public string correctCode = "6769"; // правильный код
    public KeyCode toggleUIKey = KeyCode.E; // клавиша для открытия/закрытия UI
    public float interactionDistance = 3f; // дистанция для взаимодействия

    public Rigidbody playerRigidbody;

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

        // Если Rigidbody не назначен, можно попытаться найти его автоматически
        // if (playerRigidbody == null)
        // {
        //     GameObject player = GameObject.FindGameObjectWithTag("Player");
        //     if (player != null)
        //     {
        //         playerRigidbody = player.GetComponent<Rigidbody>();
        //     }
        // }
    }

    void Update()
    {
        // Проверка, смотрите ли вы на объект и нажата ли клавиша E
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

        // Обновляем таймер, если он запущен
        if (timerRunning)
        {
            timer += Time.unscaledDeltaTime;
        }
    }

    bool IsLookingAtLock()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
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

        // Отключить Rigidbody, чтобы нельзя было двигаться
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = true;
        }

        // Сделать курсор видимым и разблокировать
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isUIActive = true;
    }

    public void HideCodeLock()
    {
        codeLockUI.SetActive(false);

        // Включить Rigidbody обратно
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false;
        }

        // Спрятать курсор и заблокировать его
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
            if (!timerRunning)
            {
                timerRunning = true;
                timer = 0f;
            }
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            inputCode = "";
            UpdateCodeDisplay();
        }
    }
}