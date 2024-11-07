using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSpeechbouble : MonoBehaviour
{
    public GameObject speech;
    public Dialogue dialogue;
    public string[] Replilk;
    public UIFloat uifloat;
    public patrol Patrol;
    
    public Transform ThisTarget;
    
   private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
            Patrol.stopAndLook(other.gameObject);
            Debug.Log("player entered");
            StartCoroutine(HandleDialogue());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("play left");
            speech.gameObject.SetActive(false);
            Patrol.resume();
        }
    }

    private IEnumerator HandleDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        speech.gameObject.SetActive(true);
        uifloat.activate(ThisTarget);
        dialogue.lines = Replilk;
        dialogue.StartDialouge();
        

    }
}
