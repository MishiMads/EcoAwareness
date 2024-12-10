using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is for playing sounds upon the player entering or exiting and are
//for a script that plays a sound upon being called see SoundEffect
public class Playsound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sound1; //sound to be played upon entering
    public AudioClip sound2; //song to be played upon leaving
    //originally the preceding sounds where different, in the final version its the same sound
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
