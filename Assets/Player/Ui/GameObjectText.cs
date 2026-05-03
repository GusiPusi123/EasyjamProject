using UnityEngine;
using UnityEngine.UI; // если используешь обычный UI Text
// using TMPro; // если TextMeshPro

public class GameObjectText : MonoBehaviour
{
    public Camera mainCamera; // главная камера
    public float interactionDistance = 3f;
    public GameObject textObject; // UI текст, который показывать
    public LayerMask interactableLayer; // слой, на котором реагировать

    void Start()
    {
        if (textObject != null)
            textObject.SetActive(false); // скрыт изначально
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            if (hit.collider != null)
            {
                // показываем текст
                if (textObject != null)
                    textObject.SetActive(true);
                return;
            }
        }
        // если не попали — скрываем текст
        if (textObject != null)
            textObject.SetActive(false);
    }
}