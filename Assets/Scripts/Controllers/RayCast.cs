using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private FishingRodSfx fishingRodSfx;
    [SerializeField] private LineRenderer lineRenderer;
    private RaycastHit hitInfo;
    public static RaycastHit castHookHitInfo;
    public static GameObject attachedObject; // Object hit by the raycast


    private void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Line Renderer settings
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.black;
    }
    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position); // Start position of the ray
        if (Input.GetMouseButtonDown(0))
        {
            Cast();
        }
        else if (Input.GetMouseButton(1))
        {
            Aim();
        }
        else
        {
            // Reset the line when not casting
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    private IEnumerator WaitForNextScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneNavigator.ToSortingScene();
    }
    private void Aim()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        // if (Physics.Raycast(ray, out hitInfo, 20f, targetLayerMask))
        if (Physics.Raycast(ray, out hitInfo, 20f))
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                // Update the line to end at the hit point
                lineRenderer.SetPosition(1, hitInfo.point);
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
            }
            else
            {
                lineRenderer.SetPosition(1, transform.position + transform.TransformDirection(Vector3.forward) * 20f);
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;
            }
        }
    }
    private void Cast()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(ray, out hitInfo, 20f))
        {
            castHookHitInfo = hitInfo;
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                attachedObject = hitInfo.collider.gameObject;
                attachedObject.transform.localRotation = Quaternion.identity;
                fishingRodSfx.PlaySuccessFishCast();
                // StartCoroutine(WaitForNextScene(fishingRodSfx.GetSuccessFishCastClip().length));
            }
            else
            {
                fishingRodSfx.PlayMissFishCast();
            }
        }
    }
}
