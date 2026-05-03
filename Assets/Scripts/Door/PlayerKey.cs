using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField] private KeyCode PickUp;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (Input.GetKeyDown(PickUp))
            {
                // Попытка использовать ключ на объекте
                var keyEvent = hit.collider.GetComponent<KeyEvent>();
                if (keyEvent != null)
                {
                    keyEvent.UnlockDoor();
                    return;
                }

                // Попытка открыть дверь
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