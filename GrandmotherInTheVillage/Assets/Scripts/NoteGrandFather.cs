using UnityEngine;

public class NoteGrandFather : MonoBehaviour
{
    public Transform player; // Трансформ игрока
    public float interactionDistance = 3f; // Расстояние для взаимодействия
    public Camera playerCamera; // Камера игрока
    public GameObject imagePanel; // Панель с изображением
    private bool isImageOpen = false;
    private bool isPaused = false;

    void Start()
    {
        if (imagePanel != null)
            imagePanel.SetActive(false); // Изначально скрыта
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionDistance)
        {
            // Проверяем, смотрит ли игрок на объект
            Vector3 directionToObject = (transform.position - playerCamera.transform.position).normalized;
            float dot = Vector3.Dot(playerCamera.transform.forward, directionToObject);

            if (dot > 0.5f) // Порог для определения, смотрит ли игрок
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ToggleImage();
                }
            }
        }
    }

    void ToggleImage()
    {
        if (imagePanel != null)
        {
            isImageOpen = !isImageOpen;
            imagePanel.SetActive(isImageOpen);

            if (isImageOpen)
            {
                // Открытие изображения — приостановка времени
                Time.timeScale = 0f;
                // Отключение управления, если нужно
                // Например, отключить управление игроком
                // player.GetComponent<PlayerController>().enabled = false;
            }
            else
            {
                // Закрытие изображения — возобновление времени
                Time.timeScale = 1f;
                // Включение управления обратно
                // player.GetComponent<PlayerController>().enabled = true;
            }
        }
    }
}

// NoteGrandFather