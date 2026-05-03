using UnityEngine;

public class NoteGrandFather : MonoBehaviour
{
    public Transform player; // Трансформ игрока
    public float interactionDistance = 3f; // Расстояние для взаимодействия
    public Camera playerCamera; // Камера игрока
    public GameObject imagePanel; // Панель с изображением
    private bool isImageOpen = false;

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

            // Увеличьте порог, чтобы быть увереннее, что игрок смотрит прямо
            if (dot > 0.99f) // более строгий порог
            {
                // Debug.Log("Игрок смотрит на объект. dot = " + dot);

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
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
// NoteGrandFather