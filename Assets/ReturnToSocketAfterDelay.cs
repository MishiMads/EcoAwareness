using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ReturnToSocketAfterDelay : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    public GameObject socket;
    
    public GameObject anchorPoint;

    [SerializeField] private float returnDelay = 2f; // Delay before returning in seconds

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grabInteractable.selectExited.AddListener(StartReturnCoroutine);

        if (anchorPoint != null)
        {
            grabInteractable.selectEntered.AddListener(UseAnchorPoint);
        }
        
    }

    private void StartReturnCoroutine(SelectExitEventArgs args)
    {
        // Start the return coroutine with a delay when the object is released
        StartCoroutine(ReturnToSocketWithDelay());
    }
    
    private void UseAnchorPoint(SelectEnterEventArgs args)
    {
        transform.position = anchorPoint.transform.position;
        transform.rotation = anchorPoint.transform.rotation;
        
        Debug.Log("virker");
    }

    private IEnumerator ReturnToSocketWithDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(returnDelay);
        
            transform.SetPositionAndRotation(socket.transform.position, socket.transform.rotation);
    }
}
