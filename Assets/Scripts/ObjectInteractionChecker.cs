using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject.name} came to the trigger area!");


        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; 
            rb.isKinematic = true; 
        }

        Vector3 targetObjectPosition = gameObject.transform.position + Vector3.up * 3f;
        StartCoroutine(MoveAndShrink(other.gameObject, targetObjectPosition));
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

        float shrinkSpeed = 1f; 
        Vector3 originalScale = obj.transform.localScale;
        while (obj != null && obj.transform.localScale.x > 0.1f)
        {
            if (obj != null) 
            {
                obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);
            }
            yield return null; 
        }

        if (obj != null) 
        {
            Destroy(obj);
        }
    }
}
