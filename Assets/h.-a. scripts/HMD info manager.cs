using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
public class HMDinfomanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("is device active" + XRSettings.isDeviceActive);
        Debug.Log("Device name is : " +XRSettings.loadedDeviceName);
        
        //checking for the device simulator
        XRDeviceSimulator xrDeviceSimulator = FindObjectOfType<XRDeviceSimulator>();
        if (xrDeviceSimulator != null && xrDeviceSimulator.isActiveAndEnabled)
        {
            Debug.Log("XR Device Simulator is active.");
        }
        else
        {
            Debug.Log("XR Device Simulator is not active.");
        }

        //checking for headset or headset simulators
        if (!XRSettings.isDeviceActive)
        {
            Debug.Log("No Headset plugged");
        }
        else if (XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "Mock HMD"||XRSettings.loadedDeviceName=="MockHMDDisplay"))
        {
            Debug.Log("Using Mock HMD");
        }
        else
        {
            Debug.Log("We Have a headset" + XRSettings.loadedDeviceName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
