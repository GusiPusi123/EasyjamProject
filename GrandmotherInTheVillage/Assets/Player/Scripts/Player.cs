// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI; // Для UI элементов

// public class Player : MonoBehaviour
// {
//     public float speed = 5;

//     [Header("Running")]
//     public bool canRun = true;
//     public bool IsRunning { get; private set; }
//     public float runSpeed = 9;
//     public float maxHP = 100f; // Максимальное здоровье
//     public string SceneDie;
//     private float currentHP;

//     public KeyCode runningKey = KeyCode.LeftShift;

//     Rigidbody rigidbody;

//     public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

//     // Для отображения HP на экране
//     public Text hpText;

//     void Start()
//     {
//         currentHP = maxHP;
//         UpdateHPText(); // Обновляем сразу при старте
//     }

//     void Awake()
//     {
//         rigidbody = GetComponent<Rigidbody>();
//     }

//     void FixedUpdate()
//     {
//         if (currentHP <= 0) return; // Не двигаться, если умер

//         IsRunning = canRun && Input.GetKey(runningKey);

//         float targetMovingSpeed = IsRunning ? runSpeed : speed;
//         if (speedOverrides.Count > 0)
//         {
//             targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
//         }

//         Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

//         rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
//     }

//     public void TakeDamage(float amount)
//     {
//         if (currentHP <= 0) return;

//         currentHP -= amount;
//         UpdateHPText();

//         if (currentHP <= 0)
//         {
//             Die();
//         }
//     }

//     void UpdateHPText()
//     {
//         if (hpText != null)
//         {
//             hpText.text = "" + Mathf.CeilToInt(currentHP).ToString();
//         }
//     }

//     void Die()
//     {
//         SceneManager.LoadScene(SceneDie);
//     }
// }


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public float maxHP = 100f; // Максимальное здоровье
    public string SceneDie;

    private float currentHP;

    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;

    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    // Для отображения HP на экране
    public Text hpText;

    // ---------------------
    // Новое: система stamina
    [Header("Stamina")]
    public Slider staminaSlider; // UI Slider для отображения stamina
    public float maxStamina = 100f; // Максимальная выносливость
    public float staminaDrainRate = 20f; // расход stamina в секунду
    public float staminaRegenRate = 10f; // восстановление stamina в секунду
    private float currentStamina;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPText();

        // Инициализация stamina
        currentStamina = maxStamina;
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (currentHP <= 0) return;

        // Проверяем, можем ли бегать
        bool canRunNow = canRun && Input.GetKey(runningKey) && currentStamina > 0;

        IsRunning = canRunNow;

        float targetMovingSpeed = IsRunning ? runSpeed : speed;

        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);

        // Обновляем stamina, если бежим
        if (IsRunning)
        {
            currentStamina -= staminaDrainRate * Time.fixedDeltaTime;
            if (currentStamina < 0)
                currentStamina = 0;
        }
        else
        {
            // Восстанавливаем stamina, если не бежим
            currentStamina += staminaRegenRate * Time.fixedDeltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }

        // Обновляем UI Slider
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }

    public void TakeDamage(float amount)
    {
        if (currentHP <= 0) return;

        currentHP -= amount;
        UpdateHPText();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "" + Mathf.CeilToInt(currentHP).ToString();
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneDie);
    }
}