using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeQuest : MonoBehaviour
{
    
    //this function is called  whenever the object becomes active, meaning that it will run when the object is set actaive
    //and when it is instatiated.
void OnEnable()
    {
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
