using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSpeechbouble : MonoBehaviour
{
    public GameObject speech; //the sppech puble 
    public Dialogue dialogue; //the script that writes dialouge and puts images in the speech puble
    public string[] Replilk; //the lines this animal has
    public UIFloat uifloat; //a script that makes ui float over a specific gameobject
    public patrol Patrol; //the patrol script on the animal this script is place on
    public bool IsTalking=false; //a bool that represents weather or not this animal is talking, the bee animation script
    //references this to check weather or not it should trigger
    
    public Transform ThisTarget;//reference for the object this script is placed on
    
   private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
            Patrol.stopAndLook(other.gameObject); //makes the animal stop and look at the player
            IsTalking = true;
            Debug.Log("player entered");
            StartCoroutine(HandleDialogue());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("play left");
            IsTalking = false;
            speech.gameObject.SetActive(false);
            Patrol.resume();
        }
    }

    private IEnumerator HandleDialogue()
    {
        //the wait was implemented so that animal would look at the player for a bit before activating the speech bubble
        yield return new WaitForSeconds(0.5f);
        speech.gameObject.SetActive(true); //sets the speechbubble as active
        uifloat.activate(ThisTarget); //puts the speech bubble over the animal
        dialogue.lines = Replilk; //feeds the speech bubble its lines
        dialogue.StartDialouge(); //tells the dialouge script to start writting/display pictures
        

    }
}
