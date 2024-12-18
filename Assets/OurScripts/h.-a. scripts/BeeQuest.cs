using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeQuest : MonoBehaviour
{
    
    //this function is called  whenever the object becomes active, meaning that it will run when the object is set actaive
    //and when it is instatiated.
void OnEnable()
    { 
        
        //OnEnable triggers when the script is set as active, meaning that when the object this script is one this
        //function will be called.
        //the following sets the bee quest as completed and records the seeds position
        QuestManager.Instance.beeQuestComplete=true;
        QuestManager.Instance.BeePlant = transform.position;
        // Find the QuestDialogue instance in the scene and call DialogueUpdate
        QuestDialogue questDialogue = FindObjectOfType<QuestDialogue>();
        if (questDialogue != null)
        {
            questDialogue.DialogueUpdate();
        }
        else
        {
            Debug.LogWarning("QuestDialogue script not found in the scene.");
        }
    }

}
