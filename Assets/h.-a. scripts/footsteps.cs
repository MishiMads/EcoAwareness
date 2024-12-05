using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class footsteps : MonoBehaviour
{
 public AudioSource walking;

 public AudioClip[] walksound;//collection of footstep sounds
 private int currentClipIndex = 0;

 public float movementThreshold = 0.01f; //how far the character needs to move before the script starts play sounds
 //this to prevent the sounds from playing when the player just leans 
 private Vector3 lastPosition;

 private float timeSinceLastStep = 0f;
 private float stepInterval = 0f; 
 private void Start()
 {
  lastPosition = transform.position;
 }

 void Update()
 {
  //compares the distance moved to the movement threshold
  float distanceMoved = Vector3.Distance(transform.position, lastPosition);
  timeSinceLastStep += Time.deltaTime;

  //if the distance moved is greather than the threshold and the interval between 
  //steps has elapsed then the function which plays a footstep sound is called and the
  //timer is reset
  if (distanceMoved > movementThreshold && timeSinceLastStep >= stepInterval)
  {
   PlayFootstepSound();
   timeSinceLastStep = 0f; // Reset timer based on the clip duration
  }
  
  lastPosition = transform.position;
 }

 private void PlayFootstepSound()
 {
  if (walksound.Length == 0) return;

  // This select a random sound from the array of footstep sounds
  int clipIndex = Random.Range(0, walksound.Length);
  AudioClip selectedClip = walksound[clipIndex];
        
  // Play the audio clip
  walking.clip = selectedClip;
  walking.Play();

  // This sets the stepinterval to be equal to the length of the footsteps audio
  //This prevent multiple footstep sounds from being played on top of each other.
  stepInterval = selectedClip.length+0.3f;
 }

}
