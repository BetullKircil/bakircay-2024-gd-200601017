using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteObjectsScript : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject selectedObject;
    private Vector3 offset;
    private bool isDrag = false;

    private Vector3 previousPosition; 
    private Vector3 currentPosition;   

    public float throwForce = 10f;  

    void Start()
    {
        mainCamera = Camera.main; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); 

            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.collider.CompareTag("movableObject")) 
                {
                    selectedObject = hit.collider.gameObject;
                    offset = selectedObject.transform.position - hit.point + Vector3.up * 10f;
                    isDrag = true; 

                    previousPosition = selectedObject.transform.position;
                }
            }
        }

        if (isDrag && selectedObject != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPos = ray.GetPoint(20); 
            selectedObject.transform.position = worldPos + offset; 

            currentPosition = selectedObject.transform.position;
        }

        if (Input.GetMouseButtonUp(0) && isDrag)
        {
            Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; 
            }

            isDrag = false;
            selectedObject = null;
        }
    }
}
