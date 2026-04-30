// using UnityEngine;

// public class FirstPersonLook : MonoBehaviour
// {
//     [SerializeField]
//     Transform character;
//     public float sensitivity = 2;
//     public float smoothing = 1.5f;

//     Vector2 velocity;
//     Vector2 frameVelocity;


//     void Reset()
//     {
//         // Get the character from the FirstPersonMovement in parents.
//         character = GetComponentInParent<Player>().transform;
//     }

//     void Start()
//     {
//         // Lock the mouse cursor to the game screen.
//         Cursor.lockState = CursorLockMode.Locked;
//     }

//     void Update()
//     {
//         // Get smooth velocity.
//         Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
//         Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
//         frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
//         velocity += frameVelocity;
//         velocity.y = Mathf.Clamp(velocity.y, -90, 90);

//         // Rotate camera up-down and controller left-right from velocity.
//         transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
//         character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
//     }
// }
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    // Добавьте ссылку на панель
    public GameObject pausePanel;
    public GameObject NoteDead;
    public GameObject NoteGrandMother;

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<Player>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Проверяем, открыта ли панель
        if (pausePanel != null && pausePanel.activeSelf)
        {
            // Если панель открыта, ничего не вращаем
            return;
        }
        // Проверяем, открыта ли панель
        if (NoteDead != null && NoteDead.activeSelf)
        {
            // Если панель открыта, ничего не вращаем
            return;
        }
        if (NoteGrandMother != null && NoteGrandMother.activeSelf)
        {
            // Если панель открыта, ничего не вращаем
            return;
        }

        // Получаем движение мыши
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Вращаем камеру вверх-вниз и персонажа по горизонтали
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}