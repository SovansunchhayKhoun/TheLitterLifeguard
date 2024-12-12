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

    [Header("Ground Check Settings")]
    public Transform groundCheck; // Empty object at the player's feet
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Layer mask to identify ground

    private float xRotation;
    public float mouseSensitivity = 1f;

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
    }

    private void HandleMovement()
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
        // Perform ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            player.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }
}
