using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public Rigidbody player;
    public float moveSpeed = 10f;
    public float rotationSpeed = 720f;
    public float jumpForce = 5f;


    [Header("Ground Check Settings")]
    public Transform groundCheck; // Empty object at the player's feet
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Layer mask to identify ground
    private bool isGrounded;


    [Header("Mouse Settings")]
    public float fppMouseSensitivity = 100f;
    public float tppMouseSensitivity = 1f;

    [Header("Camera Settings")]
    // Add the Camera and aiming logic
    public Transform playerCamera;
    public Transform thirdPersonCamera;
    public float pitchRange = 80f; // Limits for camera pitch (up/down rotation)
    public float cameraLookSpeed = 10f;
    private float xRotation;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Animator animator;

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
        fppMouseSensitivity = 150f;

    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJump();
        HandleFishing();
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

    private void HandleFishing()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("FishingCast", true);
        }
        else
        {
            animator.SetBool("FishingCast", false);
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        float magnitude = Mathf.Clamp01(direction.magnitude) * moveSpeed;
        float speed = magnitude / moveSpeed; // Normalize the speed for the animator

        // Calculate angle the player to look at
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + thirdPersonCamera.transform.eulerAngles.y;

        // Smooth the angle
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0, angle, 0);
        animator.SetFloat("Speed", speed);

        Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        transform.Translate(moveDirection.normalized * direction.magnitude * moveSpeed * Time.deltaTime, Space.World);
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
        if (CameraSwitch.isFirstPerson)
        {
            HandleFPPMouseLook();
        }
    }

    private void HandleFPPMouseLook()
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
}
