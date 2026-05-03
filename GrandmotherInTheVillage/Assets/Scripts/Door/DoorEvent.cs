using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    [SerializeField] private Animator DoorAnimator;
    [SerializeField] private bool Closed = true; // Изначально дверь закрыта

    public void TryOpen()
    {
        if (!Closed)
        {
            bool currentState = DoorAnimator.GetBool("interaction");
            DoorAnimator.SetBool("interaction", !currentState);
        }
    }

    public void Unlock()
    {
        Closed = false;
    }
}