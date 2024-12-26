using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private FishingRodSfx fishingRodSfx;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] private LineRenderer lineRenderer;
    RaycastHit hitInfo;

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
        lineRenderer.endColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        lineRenderer.SetPosition(0, transform.position); // Start position of the ray

        if (Input.GetMouseButton(0))
        {
            // animator.SetTrigger("FishingCast");
            if (Physics.Raycast(ray, out hitInfo, 20f, targetLayerMask))
            {
                // Update the line to end at the hit point
                lineRenderer.SetPosition(1, hitInfo.point);
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;

                fishingRodSfx.PlaySuccessFishCast();
                StartCoroutine(WaitForNextScene(fishingRodSfx.GetSuccessFishCastClip().length));
            }
            else
            {
                // No hit, extend line to full ray distance
                lineRenderer.SetPosition(1, transform.position + transform.TransformDirection(Vector3.forward) * 20f);
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;

                fishingRodSfx.PlayMissFishCast();
            }
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
}
