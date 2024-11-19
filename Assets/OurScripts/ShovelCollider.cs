using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShovelCollider : MonoBehaviour
{
    public GameObject shovelTip; // Reference to the shovel 
    public GameObject SoilObject; // Reference to the SoilObject
    public GameObject Ground; // Reference to Ground

    private Vector3 collisionPoint; // Holds the coordinates of where the shovel collides with the ground
    private float timer = 0f; 
    private bool inGround = false; 
    private bool delayStarted = false;

    void Update()
    {
        // Follow the shovel position and rotation
        if (shovelTip != null)
        {
            transform.position = shovelTip.transform.position;
            transform.rotation = shovelTip.transform.rotation;
        }

        // Timer logic
        if (delayStarted)
        {
            timer += Time.deltaTime; // Increment the timer
            if (timer >= 1f)
            {
                delayStarted = false; // End the delay
                timer = 0f;           // Reset the timer
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!inGround && !delayStarted)
        {
            // Check if we collided with the ground
            if (collision.gameObject == Ground)
            {
                inGround = true; // Mark as in the ground
                Debug.Log($"Collision with Ground");

                // Save the first contact point
                collisionPoint = collision.contacts[0].point;
                Debug.Log($"Collided with Ground at point: {collision.contacts[0].point}");

                // Instantiate SoilObject at collisionPoint
                Instantiate(SoilObject, collisionPoint, Quaternion.identity);
            }
            else
            {
                Debug.Log($"Did not collide with Ground");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == Ground)
        {
            inGround = false;     // No longer in the ground
            delayStarted = true;  // Start the delay
        }
    }
}

