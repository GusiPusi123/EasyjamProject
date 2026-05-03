using UnityEngine;
using System.Collections;

public class Screemer : MonoBehaviour
{
    public GameObject screemer; // Исправлено: gameObject → GameObject
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(screemer); // Исправлено: Destroy.gameObject(screemer) → Destroy(screemer)
        }
    }
}