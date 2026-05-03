// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerKey : MonoBehaviour
// {
//     [SerializeField] private KeyCode PickUp;

//     void Update()
//     {
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//         RaycastHit hit;

//         if (Physics.Raycast(ray, out hit, 3f))
//         {
//             if (Input.GetKeyDown(PickUp))
//             {
//                 // Попытка использовать ключ на объекте
//                 var keyEvent = hit.collider.GetComponent<KeyEvent>();
//                 if (keyEvent != null)
//                 {
//                     keyEvent.UnlockDoor();
//                     return;
//                 }

//                 // Попытка открыть дверь
//                 if (hit.collider.CompareTag("Door"))
//                 {
//                     var door = hit.collider.GetComponent<DoorEvent>();
//                     if (door != null)
//                     {
//                         door.TryOpen();
//                     }
//                 }
//             }
//         }
//     }
// }

using UnityEngine;
using UnityEngine.UI; // Для UI Text

public class PlayerKey : MonoBehaviour
{
    [SerializeField] private KeyCode PickUp;
    [SerializeField] private Text messageText; // ссылка на UI текст
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Отключаем сообщение по умолчанию
        if (messageText != null)
            messageText.gameObject.SetActive(false);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            bool lookingAtDoor = false;

            if (hit.collider.CompareTag("Door"))
            {
                var door = hit.collider.GetComponent<DoorEvent>();
                if (door != null)
                {
                    // Предполагаем, что если есть компонент, дверь закрыта
                    // и показываем сообщение "Дверь закрыта"
                    lookingAtDoor = true;
                }
            }

            if (lookingAtDoor && messageText != null)
            {
                messageText.gameObject.SetActive(true);
                messageText.text = "The door is closed and key is needed";
            }

            // Обработка нажатия
            if (Input.GetKeyDown(PickUp))
            {
                var keyEvent = hit.collider.GetComponent<KeyEvent>();
                if (keyEvent != null)
                {
                    keyEvent.UnlockDoor();
                    return;
                }

                if (hit.collider.CompareTag("Door"))
                {
                    var door = hit.collider.GetComponent<DoorEvent>();
                    if (door != null)
                    {
                        door.TryOpen();
                    }
                }
            }
        }
    }
}