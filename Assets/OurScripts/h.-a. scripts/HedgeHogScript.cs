using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeHogScript : MonoBehaviour
{
    void OnEnable()
    {
        //OnEnable triggers when the script is set as active, meaning that when the object this script is one this
        //function will be called.
        //the following sets the hedgehog quest as completed and records the seeds position
        QuestManager.Instance.HedgeHogQuestComplete = true;
        QuestManager.Instance.HedgeHogPlant = transform.position;
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
