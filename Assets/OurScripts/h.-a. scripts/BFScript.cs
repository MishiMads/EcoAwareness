using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFScript : MonoBehaviour
{
    void OnEnable()
    {
        QuestManager.Instance.BFQuestComplete=true;
        QuestManager.Instance.BFPlant = transform.position;
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
