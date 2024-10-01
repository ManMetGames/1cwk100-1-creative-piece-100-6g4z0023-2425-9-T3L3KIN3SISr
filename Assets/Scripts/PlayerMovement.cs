using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeedMultiplier = 2f;
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Key Bindings")]
    [SerializeField]
    private List<KeyCode> moveKeys = new List<KeyCode>
    {
        KeyCode.W, // Forward
        KeyCode.A, // Left
        KeyCode.S, // Backward
        KeyCode.D  // Right
    };
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isCrouching = false;
    private bool isGrounded;
    private float originalHeight;
    private bool isMoving = false; // To track movement

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCrouch();
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f; // Reset vertical velocity when grounded
        }

        Vector3 move = Vector3.zero;
        float currentSpeed = walkSpeed;

        // Check key bindings for movement
        if (Input.GetKey(moveKeys[0])) move += transform.forward; // W key for forward
        if (Input.GetKey(moveKeys[1])) move += -transform.right;  // A key for left
        if (Input.GetKey(moveKeys[2])) move += -transform.forward; // S key for backward
        if (Input.GetKey(moveKeys[3])) move += transform.right;  // D key for right

        // Check if sprint key is held
        if (Input.GetKey(sprintKey))
        {
            currentSpeed *= runSpeedMultiplier;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Apply gravity
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Check if the player is moving
        isMoving = move != Vector3.zero;
    }

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? crouchHeight : originalHeight;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }
    }
}
