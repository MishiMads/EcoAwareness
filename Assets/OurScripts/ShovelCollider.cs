using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShovelCollider : MonoBehaviour
{
    
    public GameObject shovelTip; // Reference to the shovel 
    public GameObject SoilObject; // Reference to the SoilObject
    public GameObject Ground; //Reference to Ground
    
    //Holds the coordinates of where tho shovel collides with the ground. 
    private Vector3 collisionPoint;

    private bool inGround;

    // Update is called once per frame
        void Update()
        {
            if (shovelTip != null)
            {
                // Update the position and rotation to match the shovel
                transform.position = shovelTip.transform.position;
                transform.rotation = shovelTip.transform.rotation;
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            //Check if the shovel is already in the ground
            if (inGround == false)
            {
                if (collision.gameObject == SoilObject)
                {
                    Debug.Log($"There is already a SoilObject there!");
                }
                
                // Check if we collided with the ground
                if (collision.gameObject == Ground)
                {
                    inGround = true;
                
                    Debug.Log($"Collision with Ground");
                
                    // Save the first contact point
                    collisionPoint = collision.contacts[0].point;
                    Debug.Log($"Collided with Ground at point: {collision.contacts[0].point}");
                
                    //Instantiate SoilObject at collisionPoint
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
                inGround = false;
            }
        }
}
