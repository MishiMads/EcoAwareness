using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSpeechbouble : MonoBehaviour
{
    public GameObject speech; //this is the speech bouble
    public Dialogue dialogue; //this the script on the speech buble that handles dialogue and pictures
    public string[] Replilk; //this is the list of lines that should be said be the animal, at pressent each animal only has one line, which is used to reference that picture that will be shown in the speech buble
    public UIFloat uifloat; //This is the script that makes the ui float above a specific npc.
    public patrol Patrol; //this is the patrol script on the animal that this script is attached to
    public bool IsTalking=false; //this bool marks weather the animal is meant to be talking, its referenced in the bee animation script
    
    public Transform ThisTarget; //A refernce to the animal this script will sit on, this used to tell the uifloat script which object it should float above
    
    //THis trigger collider wiil trigger when the player enter the animals trigger collider, it tells the patroll script 
    //that the animal should stop and look at the player, its marks the animal as talking, which the bee animation and 
    //hedgehog animation will react to. Finaly it start the HandleDialogue corutine.
   private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
            Patrol.stopAndLook(other.gameObject);
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
            IsTalking = false;//animal is not longer set as talking
            speech.gameObject.SetActive(false); //the speech buble is deactivated
            Patrol.resume(); //the patrol is set to resume
        }
    }

    //This corutine waits for half a second before activating the speech bubble, making it float above the animal,
    //changing all the lines in the dialogue script to the ones that belongs to this animal and then it fanily makes the
    //dialogue script start run through all the dialogue 
    private IEnumerator HandleDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        speech.gameObject.SetActive(true);
        uifloat.activate(ThisTarget);
        dialogue.lines = Replilk;
        dialogue.StartDialouge();
        

    }
}
