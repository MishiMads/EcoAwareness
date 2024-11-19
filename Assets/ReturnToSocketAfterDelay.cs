using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ReturnToSocketAfterDelay : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    public GameObject socket;

    [SerializeField] private float returnDelay = 2f; // Delay before returning in seconds

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

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
        
            transform.SetPositionAndRotation(socket.transform.position, socket.transform.rotation);
    }
}
