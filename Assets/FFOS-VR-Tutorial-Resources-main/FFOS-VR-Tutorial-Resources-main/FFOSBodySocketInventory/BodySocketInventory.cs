using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class bodySocket
{
    public GameObject gameObject;
    [Range(0.01f, 1f)]
    public float heightRatio;
}

public class BodySocketInventory : MonoBehaviour
{
    public GameObject HMD;
    public bodySocket[] bodySockets;
    public float offset = 5f;
    public float rotationLagFactor = 0.1f; // Lower values mean slower rotation lag
    public float rotationLimit = 45f; // Maximum rotation deviation in degrees

    private Vector3 _currentHMDlocalPosition;
    private Quaternion _currentHMDRotation;
    private Quaternion _targetBeltRotation;

    void Update()
    {
        _currentHMDlocalPosition = HMD.transform.localPosition;
        _currentHMDRotation = HMD.transform.rotation;

        // Update height of each body socket
        foreach (var bodySocket in bodySockets)
        {
            UpdateBodySocketHeight(bodySocket);
        }

        UpdateSocketInventory();
    }

    private void UpdateBodySocketHeight(bodySocket bodySocket)
    {
        bodySocket.gameObject.transform.localPosition = new Vector3(
            bodySocket.gameObject.transform.localPosition.x,
            (_currentHMDlocalPosition.y * bodySocket.heightRatio),
            bodySocket.gameObject.transform.localPosition.z
        );
    }

    private void UpdateSocketInventory()
    {
        // Update the toolbelt's position to match the HMD's position on the XZ plane
        transform.localPosition = new Vector3(
            _currentHMDlocalPosition.x,
            0, // Keep the belt at a fixed height
            _currentHMDlocalPosition.z
        );

        // Calculate the desired belt rotation with an offset
        float targetYRotation = _currentHMDRotation.eulerAngles.y + offset;

        // Clamp the rotation difference to the specified limit
        float currentYRotation = transform.rotation.eulerAngles.y;
        float rotationDifference = Mathf.DeltaAngle(currentYRotation, targetYRotation);

        if (Mathf.Abs(rotationDifference) > rotationLimit)
        {
            targetYRotation = currentYRotation + Mathf.Sign(rotationDifference) * rotationLimit;
        }

        // Interpolate towards the clamped target rotation
        _targetBeltRotation = Quaternion.Euler(0, targetYRotation, 0);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            _targetBeltRotation,
            rotationLagFactor * Time.deltaTime
        );
        
    }
}