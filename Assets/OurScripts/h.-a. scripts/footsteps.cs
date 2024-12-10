using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class footsteps : MonoBehaviour
{
 public AudioSource walking; //the audio source plays audio in the scene

 public AudioClip[] walksound;//a list of footstep audio sounds
 //private int currentClipIndex = 0;

 public float movementThreshold = 0.01f; //how far the player has to move each frame to trigger a foot sound
 private Vector3 lastPosition; //recording the position of the players position last frame for comparison

 private float timeSinceLastStep = 0f; //how long its been since last time a footstep sounds was started
 private float stepInterval = 0f; //how long it needs to be before a new foot step sound can be played, this is determind
 //by the length of the audio  being played
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
