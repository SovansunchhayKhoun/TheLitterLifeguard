using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float moveSpeed = 10f;
    private Animator animator;


    void Start()
    {
        player = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

        // Set animation speed parameter based on movement
        float speed = movement.magnitude;
        animator.SetFloat("Speed", speed);

        // Move the game object
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
