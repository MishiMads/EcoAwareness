using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private InputActionProperty pokeAction;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();
        float pokeValue = pokeAction.action.ReadValue<float>();
        
        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
        animator.SetFloat("Poke", pokeValue);
    }
}
