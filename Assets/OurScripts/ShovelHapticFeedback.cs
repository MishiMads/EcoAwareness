using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class ShovelHapticFeedback : MonoBehaviour
{
    public HapticImpulsePlayer controller; // The XR Controller to send haptics

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Trigger haptic feedback
            if (controller != null)
            {
                controller.SendHapticImpulse(0.5f, 0.8f); // Amplitude and duration
            }
        }
    }
}



