using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float moveSpeed = 10f;
    public float rotationSpeed = 720f;
    private Animator animator;


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
    }

    void Update()
    {
        HandleMovement();
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
        float speed = movement.magnitude;
        animator.SetFloat("Speed", speed);

        // Move the game object
        Vector3 velocity = movement * magnitude;
        transform.Translate(velocity * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
