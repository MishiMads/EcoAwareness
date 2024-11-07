using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class pick_up : MonoBehaviour
{
    public Transform handTransform; // The hand or controller position
    public float rayDistance = 2.0f; // How far the ray can reach
    public LayerMask grabbableLayer; // Layer for grabbable objects
    public XRNode inputSource; // Left or Right hand controller

    private InputDevice targetDevice;
    private GameObject heldObject = null;
    private Rigidbody heldObjectRb;
    private bool isHolding = false;
    private Vector3 previousPosition;

    void Start()
    {
        // Get the target device (Left or Right hand controller)
        targetDevice = InputDevices.GetDeviceAtXRNode(inputSource);
        
        if (targetDevice.isValid)
        {
            Debug.Log("Input device initialized for " + inputSource);
        }
        else
        {
            Debug.LogWarning("Input device not found for " + inputSource);
        }
    }

    void Update()
    {
        // Check if the player is trying to grab
        targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool isGripping);
        if (isGripping)
        {
            Debug.Log("grip button pressed");
        }
        if (isGripping && !isHolding)
        {
            TryPickupObject();
        }
        else if (!isGripping && isHolding)
        {
            ThrowObject();
        }

        if (isHolding)
        {
            // Move the object with the hand while holding it
            heldObject.transform.position = handTransform.position;

            // Save the previous position for velocity calculation
            previousPosition = handTransform.position;
        }
    }

    void TryPickupObject()
    {
        // Raycast to detect objects in front of the hand
        Ray ray = new Ray(handTransform.position, handTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, grabbableLayer))
        {
            if (hit.collider != null && hit.collider.GetComponent<Rigidbody>() != null)
            {
                // Attach the object to the hand
                heldObject = hit.collider.gameObject;
                heldObjectRb = heldObject.GetComponent<Rigidbody>();

                // Disable the object's physics while holding
                heldObjectRb.isKinematic = true;

                // Mark as holding
                isHolding = true;

                // Store the initial position
                previousPosition = handTransform.position;
            }
        }
    }

    void ThrowObject()
    {
        // Release the object
        heldObjectRb.isKinematic = false;

        // Calculate the velocity for throwing
        Vector3 throwVelocity = (handTransform.position - previousPosition) / Time.deltaTime;
        heldObjectRb.velocity = throwVelocity;

        // Clear held object
        heldObject = null;
        heldObjectRb = null;

        // Mark as not holding
        isHolding = false;
    }

}
