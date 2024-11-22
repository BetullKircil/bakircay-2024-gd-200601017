using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionChecker2 : MonoBehaviour
{
    private GameObject storedObject; 
                                    
    private void OnTriggerEnter(Collider other)
    {
        if (storedObject == null)
        {
            storedObject = other.gameObject;
            Debug.Log($"{storedObject.name} is located to area.");
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false; 
                rb.isKinematic = true; 
            }

            Collider col = storedObject.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false; 
            }

            Vector3 targetPosition = gameObject.transform.position + Vector3.up * 3f;
            StartCoroutine(MoveAndShrink(other.gameObject, targetPosition));
        }
        else
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 forceDirection = (other.transform.position - transform.position).normalized;
                if (forceDirection.magnitude < 0.5f) 
                {
                    forceDirection = Vector3.back.normalized;
                }
                rb.AddForce(forceDirection * 1000f); 
                Debug.Log($"{other.gameObject.name} throwed cause of the area is full.");
            }
        }
    }

    private IEnumerator MoveAndShrink(GameObject obj, Vector3 targetPosition)
    {
        float moveSpeed = 2f;
        while (obj != null && Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            if (obj != null) 
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            yield return null; 
        }

        if (obj == null) yield break;
    }
}
