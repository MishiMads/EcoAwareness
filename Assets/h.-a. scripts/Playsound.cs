using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playsound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sound1;
    public AudioClip sound2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Playing sound");
            audioSource.clip = sound1;
            audioSource.Play();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.clip = sound2;
            audioSource.Play();
        }
    }

 
}
