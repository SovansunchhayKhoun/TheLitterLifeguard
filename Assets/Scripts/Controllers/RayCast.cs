using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
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
                SceneNavigator.ToSortingScene();
            }
            else
            {
                Debug.Log("Nothing");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);
            }
        }
    }
}
