using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent : MonoBehaviour
{
    [SerializeField] private DoorEvent Door;

    public void UnlockDoor()
    {
        if (Door != null)
        {
            Door.Unlock();
            Destroy(gameObject);
        }
    }
}