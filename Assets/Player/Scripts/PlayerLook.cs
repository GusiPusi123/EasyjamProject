using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    // Ссылки на панели и объекты
    public GameObject pausePanel;
    public GameObject NoteDead;
    public GameObject NoteGrandMother;
    public GameObject CodeLockPanel;
    public GameObject Code1;
    public GameObject Code2;
    public GameObject Code3;
    public GameObject Code4;
    public GameObject Note1;
    public GameObject Check;

    void Reset()
    {
        // Получить персонажа из родительского компонента
        character = GetComponentInParent<Player>().transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Проверяем, активны ли панели или есть активные записки
        if ((pausePanel != null && pausePanel.activeSelf) ||
            (NoteDead != null && NoteDead.activeSelf) ||
            (NoteGrandMother != null && NoteGrandMother.activeSelf) ||
            (CodeLockPanel != null && CodeLockPanel.activeSelf) ||
            (Code1 != null && Code1.activeSelf) ||
            (Code2 != null && Code2.activeSelf) ||
            (Code3 != null && Code3.activeSelf) ||
            (Code4 != null && Code4.activeSelf) ||
            (Note1 != null && Note1.activeSelf) ||
            (Check != null && Check.activeSelf))
        {
            return;
        }

        // Получение движения мыши
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Вращение камеры по вертикали
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        // Вращение персонажа по горизонтали
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}