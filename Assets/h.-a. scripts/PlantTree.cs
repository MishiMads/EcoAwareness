using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a class that holds a tag and the associated prefab
[System.Serializable] //making it serialized allows the class to show up in the inspector
public class TaggedPrefab
{
    public string tag;
    public GameObject prefab;
}

public class PlantTree : MonoBehaviour
{
    // This list holds all the prefabs and their associated tag
    public List<TaggedPrefab> taggedPrefabs;

    // This pool checks weather or not something has been planted
    private bool earthInUse = false;

    private void Start()
    {
      
    }
    //this loop triggers upon a collision with and object
    private void OnCollisionEnter(Collision collider)
    {
        // Check the list of prefabs for a prefab with a matching tag
        foreach (TaggedPrefab item in taggedPrefabs)
        {
            if (collider.gameObject.CompareTag(item.tag) && !earthInUse)
            {
                // Set earth as being used
                earthInUse = true;

                // This destroys the colliding object 
                Destroy(collider.gameObject);

                // instantiate the associated prefab
                PlantPrefabAtPosition(item.prefab, gameObject.transform.position);

                // This ends the loop 
                break;
            }
        }
    }

    // This method instatiates the prefab, after the loop has determined which prefab needs to be instantiated.
    private void PlantPrefabAtPosition(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}