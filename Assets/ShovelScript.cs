using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelScript : MonoBehaviour
{
    public GameObject Shovel; // Reference to the shovel GameObject
    public GameObject DirtMoundPrefab; // Prefab for the dirt mound to instantiate
    public Transform PlayerHand; // Reference to the player's hand (for checking if the shovel is held)
    
    private List<Vector3> dirtMoundPositions = new List<Vector3>(); // List to store dirt mound positions

    private bool IsShovelInHand()
    {
        // Check if the shovel is in the player's hand
        return Shovel.transform.IsChildOf(PlayerHand);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the shovel collides with the ground
        if (collision.gameObject.CompareTag("Ground") && IsShovelInHand())
        {
            InstantiateDirtMound();
        }
    }

    public void InstantiateDirtMound()
    {
        // Get the position of the shovel and create a dirt mound
        Vector3 moundPosition = Shovel.transform.position;

        // Instantiate the dirt mound prefab
        GameObject dirtMound = Instantiate(DirtMoundPrefab, moundPosition, Quaternion.identity);

        // Store the position in the list
        dirtMoundPositions.Add(moundPosition);

        Debug.Log($"Dirt mound created at: {moundPosition}");
    }
}