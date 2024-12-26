using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private FishingRodSfx fishingRodSfx;
    [SerializeField] LayerMask targetLayerMask;
    RaycastHit hitInfo;
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hitInfo, 20f, targetLayerMask))
            {
                Debug.Log("Hit something", hitInfo.collider.gameObject);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);
                fishingRodSfx.PlaySuccessFishCast();

                // wait for 1 second before moving to the next scene
                StartCoroutine(WaitForNextScene(fishingRodSfx.GetSuccessFishCastClip().length));
            }
            else
            {
                Debug.Log("Nothing");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);
                fishingRodSfx.PlayMissFishCast();
            }
        }
    }

    private IEnumerator WaitForNextScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneNavigator.ToSortingScene();
    }
}
