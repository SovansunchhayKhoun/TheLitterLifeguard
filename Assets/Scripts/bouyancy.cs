using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public Transform waterSurface;  // Reference to the water surface.
    public float waterDensity = 1000f;  // Density of the water.
    public float objectVolume = 1f;  // Volume of the object.
    public float drag = 0.1f;  // Drag force.
    public float angularDrag = 0.05f;  // Angular drag.

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        rb.angularDrag = angularDrag;
    }

    void FixedUpdate()
    {
        // Calculate the submerged portion of the object
        float waterLevel = waterSurface.position.y;
        float objectDepth = Mathf.Clamp01((waterLevel - transform.position.y) / transform.localScale.y);

        // Calculate buoyancy force
        Vector3 buoyancyForce = Vector3.up * waterDensity * objectVolume * objectDepth * Physics.gravity.magnitude;

        // Apply buoyancy force
        rb.AddForce(buoyancyForce, ForceMode.Force);
    }
}
