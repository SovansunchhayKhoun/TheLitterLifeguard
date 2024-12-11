using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float moveSpeed = 10f;
    public float rotationSpeed = 720f;
    public float jumpForce = 5f;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        player = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        moveSpeed = 10f;
        rotationSpeed = 720f;
        jumpForce = 5f;
        isGrounded = true;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        // HandleMouseMovement();
    }

    private void HandleMovement()
    {
        // Get input from the user
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down arrow keys

        // Create a movement vector
        Vector3 movement = new(moveX, 0, moveZ);
        float magnitude = Mathf.Clamp01(movement.magnitude) * moveSpeed;
        movement.Normalize();

        // Set animation speed parameter based on movement
        float speed = magnitude / moveSpeed; // Normalize the speed for the animator
        animator.SetFloat("Speed", speed);

        // Apply movement
        Vector3 velocity = movement * magnitude;
        transform.Translate(velocity * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Apply jump force
            player.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("Jump", true);

            // Set grounded status to false
            isGrounded = false;
        }
    }
    private float xRotation;
    public float mouseSensitivity = 1f;
    private void HandleMouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player has landed on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
