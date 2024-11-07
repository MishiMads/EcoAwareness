using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFloat : MonoBehaviour
{

    public Transform target;// The target GameObject the UI element will float above
    public Vector3 worldOffset = new Vector3(0.1f, 2, 0); // Offset to position UI above target
    public Transform UItransform; // Transform of the UI element
    public Camera mainCamera; // The camera the player uses
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void activate(Transform Target)
    {
        target = Target;
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (target != null && mainCamera != null)
        {
            // Make the UI element face the camera
            UItransform.rotation = Quaternion.LookRotation(UItransform.position - mainCamera.transform.position);

            // Position the UI element above the target, with the specified offset
            UItransform.position = target.position + worldOffset;
        }

    }
}
