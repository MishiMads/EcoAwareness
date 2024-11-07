using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ReturnToSocketAfterDelay : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Transform socketTransform;

    [SerializeField] private float returnDelay = 2f; // Delay before returning in seconds

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        
        // Store the initial position and rotation of the object
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Check if it's in a socket initially and store that socket's transform
        if (grabInteractable.firstInteractorSelecting is UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket)
        {
            socketTransform = socket.transform;
        }
        
        grabInteractable.selectExited.AddListener(StartReturnCoroutine);
    }

    private void StartReturnCoroutine(SelectExitEventArgs args)
    {
        // Start the return coroutine with a delay when the object is released
        StartCoroutine(ReturnToSocketWithDelay());
    }

    private IEnumerator ReturnToSocketWithDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(returnDelay);

        // Return to the socket if socketTransform exists
        if (socketTransform != null)
        {
            transform.SetPositionAndRotation(socketTransform.position, socketTransform.rotation);
        }
        else
        {
            // If no socket, return to the original position
            transform.SetPositionAndRotation(originalPosition, originalRotation);
        }
    }
}
