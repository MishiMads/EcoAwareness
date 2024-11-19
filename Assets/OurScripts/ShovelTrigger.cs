using UnityEngine;

public class ShovelTrigger : MonoBehaviour
{
    public GameObject shovelTip; // Reference to the shovel tip
    public GameObject SoilObject; // Reference to the SoilObject prefab

    private Vector3 collisionPoint; // Holds the coordinates of collision

    void Update()
    {
        if (shovelTip != null)
        {
            // Sync the position and rotation of this object with the shovel tip
            transform.position = shovelTip.transform.position;
            transform.rotation = shovelTip.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log($"Trigger detected with: {other.gameObject.name}");
            
            // Save the collision point and instantiate the soil object
            collisionPoint = transform.position; 
            Debug.Log($"Ground trigger at position: {transform.position}");
            Instantiate(SoilObject, collisionPoint, Quaternion.identity);
        }
        else
        {
            Debug.Log($"Did not trigger with ground");
        }
    }
}

