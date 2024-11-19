using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelCollider : MonoBehaviour
{
    
    public GameObject shovel; // Reference to the shovel 
    public GameObject SoilObject; // Reference to the SoilObject 
    
    //Holds the coordinates of where tho shovel collides with the ground. 
    private Vector3 collisionPoint; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
        void Update()
        {
            if (shovel != null)
            {
                // Update the position and rotation to match the shovel
                transform.position = shovel.transform.position;
                transform.rotation = shovel.transform.rotation;
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            // Check if we collided with the ground
            if (collision.gameObject.CompareTag("Ground"))
            {
                // Save the first contact point
                collisionPoint = collision.contacts[0].point;
                Instantiate(SoilObject, collisionPoint, Quaternion.identity);
            }
        }
}
