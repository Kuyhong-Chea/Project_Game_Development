using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform cam;
    public float speed = 2.5f;
    public float mouseSpeed = 1f;
    public float speedMultiplier = 1f;

    [Header("Sprint")]
    public float maxStamina = 3f;
    public float staminaRegen = 0.6f;
    public Slider staminaBar;

    CharacterController controller;
    float lookUpDown = 0f;
    float stamina;
    bool exhausted = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        stamina = maxStamina;
    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSpeed, 0);

        lookUpDown -= Input.GetAxis("Mouse Y") * mouseSpeed;
        lookUpDown = Mathf.Clamp(lookUpDown, -80, 80);
        cam.localRotation = Quaternion.Euler(lookUpDown, 0, 0);

        Vector3 move = transform.right * Input.GetAxis("Horizontal")
                     + transform.forward * Input.GetAxis("Vertical");

        bool sprintPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(0);
        bool sprinting = sprintPressed && move.magnitude > 0.1f && !exhausted;

        if (sprinting)
        {
            stamina -= Time.deltaTime;
            if (stamina <= 0f)
            {
                stamina = 0f;
                exhausted = true;
            }
        }
        else
        {
            stamina += staminaRegen * Time.deltaTime;
            if (stamina > maxStamina) stamina = maxStamina;

            if (exhausted && stamina >= maxStamina * 0.3f)
            {
                exhausted = false;
            }
        }

        float currentSpeed = speed * speedMultiplier;
        if (sprinting)
        {
            currentSpeed = currentSpeed * 2f;
        }

        controller.SimpleMove(move * currentSpeed);

        if (staminaBar != null)
        {
            staminaBar.value = stamina / maxStamina;
        }
    }
}