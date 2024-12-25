using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float moveSpeed = 10f;
    public float rotationSpeed = 720f;
    public float jumpForce = 5f;
    private Animator animator;

    [Header("Ground Check Settings")]
    public Transform groundCheck; // Empty object at the player's feet
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Layer mask to identify ground

    private bool isGrounded;
    private float xRotation;
    public float fppMouseSensitivity = 100f;
    public float tppMouseSensitivity = 1f;

    // Add the Camera and aiming logic
    public Transform playerCamera;
    public float pitchRange = 80f; // Limits for camera pitch (up/down rotation)

    [Header("Cinemachine Camera Settings")]
    public CinemachineFreeLook thirdPersonCamera;
    public float cameraLookSpeed = 10f;

    void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        player = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        // playerCamera = Camera.main.transform; // Use the main camera for first-person view

        Cursor.lockState = CursorLockMode.Locked; // Locks cursor to the center of the screen
        Cursor.visible = false; // Hides the cursor

        moveSpeed = 10f;
        rotationSpeed = 720f;
        jumpForce = 5f;
        fppMouseSensitivity = 100f;

        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.m_XAxis.m_MaxSpeed = cameraLookSpeed;
            thirdPersonCamera.m_YAxis.m_MaxSpeed = cameraLookSpeed;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJump();
    }

    private void HandleMovement()
    {
        if (CameraSwitch.isFirstPerson)
        {
            HandleFPPMovement();
        }
        else
        {
            HandleTPPMovement();
        }
    }

    private void HandleFPPMovement()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down

        // Calculate movement direction
        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        float magnitude = Mathf.Clamp01(movement.magnitude) * moveSpeed;
        movement.Normalize();

        float speed = magnitude / moveSpeed; // Normalize the speed for the animator
        animator.SetFloat("Speed", speed);

        // Move the player
        Vector3 velocity = movement * magnitude;
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    private void HandleTPPMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys

        Vector3 movement = new(moveX, 0, moveZ);
        float magnitude = Mathf.Clamp01(movement.magnitude) * moveSpeed;
        movement.Normalize();

        float speed = magnitude / moveSpeed; // Normalize the speed for the animator
        animator.SetFloat("Speed", speed);

        Vector3 velocity = movement * magnitude;
        transform.Translate(velocity * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void HandleJump()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            player.velocity = new Vector3(player.velocity.x, jumpForce, player.velocity.z);
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    private void HandleMouseLook()
    {
        // Rotate the camera (pitch) based on mouse Y input (up and down)
        if (CameraSwitch.isFirstPerson)
        {
            float mouseX = Input.GetAxis("Mouse X") * fppMouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * fppMouseSensitivity * Time.deltaTime;

            // Rotate camera up/down (pitch)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -pitchRange, pitchRange); // Clamping the pitch to avoid extreme angles

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Apply the pitch

            // Rotate the player (Yaw) based on mouse X input (left and right)
            transform.Rotate(Vector3.up * mouseX); // Yaw
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X") * tppMouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * tppMouseSensitivity;

            transform.Rotate(Vector3.up * mouseX); // Rotate player for yaw

            // Control horizontal (yaw) rotation:
            thirdPersonCamera.m_XAxis.Value += mouseX * Time.deltaTime;

            // Control vertical (pitch) rotation:
            thirdPersonCamera.m_YAxis.Value -= mouseY * Time.deltaTime;

            thirdPersonCamera.m_YAxis.Value = Mathf.Clamp(thirdPersonCamera.m_YAxis.Value, 0.3f, 0.8f);
        }
    }
}
