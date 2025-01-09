using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Inspiration from https://www.reddit.com/r/Unity3D/comments/1kx1hp/best_way_to_do_fishing_line/
public class VerletLine : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public int Segments = 10;
    public LineRenderer lineRenderer;
    public float SegmentLength = 0.03f;
    public float startSegmentLength = 0.03f;
    public float currentTargetLength = 0.03f;
    public float maxSegmentLength = 0.4f;
    public Vector3 Gravity = new Vector3(0, -9.81f, 0);
    // Num of Physics iterations
    public int Iterations = 6;
    // higher is stiffer, lower is stretchier
    public float tensionConstant = 10f;
    public bool SecondHasRigidbody = false;
    public float LerpSpeed = 1f;
    public float Delay = 0.5f;
    private bool isChangingLength = false;
    public GameObject MarkerPrefab; // Drag a marker prefab (like a sphere) here in the inspector.
    private GameObject markerInstance;
    private bool isReeling = false;

    // Represents a segment of the line.
    private class LineParticle
    {
        public Vector3 Pos;
        public Vector3 OldPos;
        public Vector3 Acceleration;
    }

    private List<LineParticle> particles;
    // Initializes the line.
    void Start()
    {
        InitVerletLine();
        particles = new List<LineParticle>();
        for (int i = 0; i < Segments; i++)
        {
            Vector3 point = Vector3.Lerp(StartPoint.position, EndPoint.position, i / (float)(Segments - 1));
            particles.Add(new LineParticle { Pos = point, OldPos = point, Acceleration = Gravity });
        }
        lineRenderer.positionCount = particles.Count;

        // Instantiate marker and deactivate it initially
        if (MarkerPrefab)
        {
            markerInstance = Instantiate(MarkerPrefab);
            markerInstance.SetActive(false);
        }
    }
    void Update()
    {
        RaycastHit _rayCastHit = RayCast.hitInfo;
        GameObject _attachedObject = RayCast.attachedObject;

        if (!isReeling)
        {
            EndPoint.position = _rayCastHit.point; // Update the endpoint position
        }

        if (Input.GetMouseButtonDown(0)) // On left-click, cast
        {
            EndPoint.position = _rayCastHit.point; // Update the endpoint position
            isReeling = false;
        }
        else if (Input.GetKey(KeyCode.Q)) // On "Q", reel in
        {
            currentTargetLength = startSegmentLength;
            isChangingLength = true;
            isReeling = true;

            if (_rayCastHit.point != new Vector3(0, 0, 0))
            {
                EndPoint.position = _rayCastHit.point; // Update the endpoint position
                // Move and show the marker at the hit position
                if (markerInstance)
                {
                    markerInstance.transform.position = _rayCastHit.point;
                    markerInstance.SetActive(true);
                }
            }
        }

        // Adjust line length smoothly
        if (isChangingLength)
        {
            SegmentLength = Mathf.Lerp(SegmentLength, currentTargetLength, LerpSpeed * Time.deltaTime);

            if (Mathf.Abs(SegmentLength - currentTargetLength) < 0.01f)
            {
                SegmentLength = currentTargetLength;
                isChangingLength = false;

                // If reeling in, attach the hit object to the endpoint
                if (_attachedObject != null && SegmentLength == startSegmentLength)
                {
                    AttachTrash(_attachedObject);
                }
            }
        }
    }

    private void AttachTrash(GameObject attachedObject)
    {
        var originalLossyScale = attachedObject.transform.lossyScale; // Store lossy scale

        attachedObject.transform.SetParent(EndPoint);
        attachedObject.transform.localPosition = Vector3.zero; // Reset local position
        attachedObject.transform.localRotation = Quaternion.identity; // Reset local rotation

        // Calculate the local scale to maintain the original lossy scale
        attachedObject.transform.localScale = new Vector3(
            originalLossyScale.x / EndPoint.lossyScale.x,
            originalLossyScale.y / EndPoint.lossyScale.y,
            originalLossyScale.z / EndPoint.lossyScale.z
        );

        // disable the attached object's Rigidbody to prevent physics interference
        Rigidbody attachedRigidbody = attachedObject.GetComponent<Rigidbody>();
        if (attachedRigidbody != null)
        {
            attachedRigidbody.isKinematic = true;
        }
    }

    private void InitVerletLine()
    {
        Delay = 1.5f;
        LerpSpeed = 1f;
        tensionConstant = 1000f;
    }

    // Update the line with Verlet Physics.
    void FixedUpdate()
    {

        foreach (var p in particles)
        {
            Verlet(p, Time.fixedDeltaTime);
        }

        for (int i = 0; i < Iterations; i++)
        {
            for (int j = 0; j < particles.Count - 1; j++)
            {
                PoleConstraint(particles[j], particles[j + 1], SegmentLength);
            }
        }
        particles[0].Pos = StartPoint.position;
        if (SecondHasRigidbody)
        {
            Vector3 force = (particles[particles.Count - 1].Pos - EndPoint.position) * tensionConstant;
            EndPoint.GetComponent<Rigidbody>().AddForce(force);
        }

        particles[particles.Count - 1].Pos = EndPoint.position;

        var positions = new Vector3[particles.Count];
        for (int i = 0; i < particles.Count; i++)
        {
            positions[i] = particles[i].Pos;
        }
        lineRenderer.SetPositions(positions);
    }

    // Performs Verlet integration to update the position of a particle.
    private void Verlet(LineParticle p, float dt)
    {
        var temp = p.Pos;
        p.Pos += p.Pos - p.OldPos + (p.Acceleration * dt * dt);
        p.OldPos = temp;
    }

    // Applies a pole constraint to a pair of particles.
    // We want the distance between each particle to be a specific length
    private void PoleConstraint(LineParticle p1, LineParticle p2, float restLength)
    {
        var delta = p2.Pos - p1.Pos;
        var deltaLength = delta.magnitude;
        var diff = (deltaLength - restLength) / deltaLength;
        p1.Pos += delta * diff * 0.5f;
        p2.Pos -= delta * diff * 0.5f;
    }
}