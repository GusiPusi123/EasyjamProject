using UnityEngine;

public class CabinetDoor : MonoBehaviour
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
                if (hit.collider.CompareTag("CabinetDoor"))
                {
                    Animator doorAnimator = hit.collider.GetComponent<Animator>();
                    if (doorAnimator != null)
                    {
                        // Проверка текущего состояния двери
                        AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
                        // Названия состояний должны совпадать с названиями в анимации
                        if (stateInfo.IsName("DoorLitleClose"))
                        {
                            doorAnimator.SetTrigger("Open");
                        }
                        else if (stateInfo.IsName("DoorLitleOpen"))
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