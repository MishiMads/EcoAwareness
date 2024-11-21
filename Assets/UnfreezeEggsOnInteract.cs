using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class UnfreezeEggsOnInteract: MonoBehaviour
{
    public Rigidbody rigidbody;
    public XRGrabInteractable grabInteractable;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
    }
    
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        rigidbody.constraints = RigidbodyConstraints.None;
    }
}
