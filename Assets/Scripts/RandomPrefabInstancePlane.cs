using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabInstancePlane : MonoBehaviour
{
    public GameObject[] prefabs;
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            if (prefabs.Length == 0 || gameObject == null)
            {
                Debug.LogError("Prefabs or Planes are not defined!");
                return;
            }
            Debug.Log(gameObject.transform.localScale.z + " - " + gameObject.transform.localScale.x);
            float areaWidth = gameObject.transform.localScale.x * 10; 
            float areaHeight = gameObject.transform.localScale.z * 10; 

            Vector3 areaPosition = gameObject.transform.position; 
            Debug.Log(areaPosition + " - " + areaWidth
                 + " - " + areaHeight);

            float randomX = Random.Range(areaPosition.x, areaPosition.x + areaWidth / 2);
            float randomZ = Random.Range(areaPosition.z - areaHeight / 2, areaPosition.z + areaHeight / 2);

            float yPosition = areaPosition.y + 5;

            Vector3 randomPosition = new Vector3(randomX, yPosition, randomZ);
            Debug.Log("the random Position " + i + ". obje : " + randomPosition);
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject selectedPrefab = prefabs[randomIndex];

            Instantiate(selectedPrefab, randomPosition, Quaternion.identity);
        }

    }
}
