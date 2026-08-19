using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float sprintMult = 2f;

    [Header("Look Sensitivity")]
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float lookAngleClamp = 80f;

    [Header("Input Settings")]
    [SerializeField] private string horizontalMoveInput = "Horizontal";
    [SerializeField] private string verticalMoveInput = "Vertical";
    [SerializeField] private string MouseXInput = "Mouse X";
    [SerializeField] private string MouseYInput = "Mouse Y";
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    Camera mainCamera;
    float verticalRotation;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
    }

    void HandleMovement()
    {
        float speedMult = Input.GetKey(sprintKey) ? sprintMult : 1f; // I hate ternary operators (?)

        float verticalSpeed = Input.GetAxis(verticalMoveInput) * walkSpeed * speedMult;
        float horizontalSpeed = Input.GetAxis(horizontalMoveInput) * walkSpeed * speedMult;

        Vector3 speed = new Vector3 (horizontalSpeed, 0, verticalSpeed);
        speed = transform.rotation * speed;

        characterController.SimpleMove(speed);
    }

    void HandleLook()
    {
        float mouseXRotation = Input.GetAxis(MouseXInput) * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= Input.GetAxis(MouseYInput) * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -lookAngleClamp, lookAngleClamp);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
