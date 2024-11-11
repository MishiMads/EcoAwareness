using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class footsteps : MonoBehaviour
{
 public AudioSource walking;

 public AudioClip[] walksound;
 private int currentClipIndex = 0;

 public float movementThreshold = 0.01f;
 private Vector3 lastPosition;

 private float timeSinceLastStep = 0f;
 private float stepInterval = 0f; 
 private void Start()
 {
  lastPosition = transform.position;
 }

 void Update()
 {
  float distanceMoved = Vector3.Distance(transform.position, lastPosition);
  timeSinceLastStep += Time.deltaTime;

  
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

  // Select a random sound from the array of footstep sounds
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
