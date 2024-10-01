using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeedMultiplier = 2f;
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Mouse Look Settings")]
    [SerializeField] private float mouseSensitivity = 100f; // Sensitivity for looking around
    [SerializeField] private Transform playerCamera; // The camera attached to the player
    private float xRotation = 0f; // Track vertical camera rotation

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isCrouching = false;
    private bool isGrounded;
    private float originalHeight;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCrouch();
        HandleMouseLook(); // Call to handle looking around
        HandleVibration(); // Call to handle vibration when moving
    }

    private void HandleMovement()
    {
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;

        // Reset vertical velocity when grounded
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Get movement input
        Vector2 moveInput = Gamepad.current.leftStick.ReadValue();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = transform.TransformDirection(move); // Convert local move direction to world space

        // Determine current speed based on sprinting
        float currentSpeed = walkSpeed;

        // Check if the L3 button (buttonWest) is pressed for sprinting
        if (Gamepad.current.buttonWest.isPressed && moveInput.magnitude > 0.1f) // Only sprint if L3 is pressed and there is movement
        {
            currentSpeed *= runSpeedMultiplier; // Apply sprint speed
        }

        // Move the player
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Apply gravity
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleCrouch()
    {
        if (Gamepad.current.buttonEast.wasPressedThisFrame) // Typically B on Xbox, Circle on PlayStation
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? crouchHeight : originalHeight; // Toggle crouch height
        }
    }

    private void HandleJump()
    {
        // Check if the buttonSouth (X button on DualSense) is pressed and the player is grounded
        if (Gamepad.current.buttonSouth.wasPressedThisFrame && isGrounded) // X on PlayStation
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y); // Apply jump force
        }
    }

    private void HandleMouseLook()
    {
        // Get right stick input for looking around
        Vector2 lookInput = Gamepad.current.rightStick.ReadValue();

        // Rotate the player around the Y-axis
        transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity * Time.deltaTime);

        // Handle vertical camera rotation
        xRotation -= lookInput.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical rotation

        // Apply the clamped rotation to the camera
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void HandleVibration()
    {
        if (controller.isGrounded && Gamepad.current.leftStick.ReadValue().magnitude > 0.1f)
        {
            // Slight vibration when moving
            Gamepad.current.SetMotorSpeeds(0.2f, 0.2f); // Adjust the values for left and right motor
        }
        else
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f); // Stop vibration when not moving
        }
    }
}
