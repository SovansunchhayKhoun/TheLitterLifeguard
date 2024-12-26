using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatTrash : MonoBehaviour
{
    public float amplitude = 0.07f; // Height of the floating motion
    public float frequency = 1f;  // Speed of the floating motion

    void Update()
    {
        // Vertical floating motion
        Vector3 pos = transform.position;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime;
        transform.position = pos;

        // Optional: Add a gentle rotation
        transform.Rotate(0, Mathf.Sin(Time.time * frequency) * amplitude, 0);
    }
}

