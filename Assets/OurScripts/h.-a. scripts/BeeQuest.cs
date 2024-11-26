using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeQuest : MonoBehaviour
{

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
