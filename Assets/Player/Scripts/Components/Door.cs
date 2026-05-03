using UnityEngine;

public class Door : MonoBehaviour
{
    public float interactionDistance = 3f; // Дальность взаимодействия
    public KeyCode interactKey = KeyCode.E; // Клавиша взаимодействия
    public Transform playerCamera; // Камера игрока

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    Animator doorAnimator = hit.collider.GetComponent<Animator>();
                    if (doorAnimator != null)
                    {
                        // Проверка текущего состояния двери
                        AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
                        // Названия состояний должны совпадать с названиями в анимации
                        if (stateInfo.IsName("DoorClose"))
                        {
                            doorAnimator.SetTrigger("Open");
                        }
                        else if (stateInfo.IsName("DoorOpen"))
                        {
                            doorAnimator.SetTrigger("Close");
                        }
                        else
                        {
                            // Если состояние неизвестное, просто открыть
                            doorAnimator.SetTrigger("Open");
                        }
                    }
                }
            }
        }
    }
}