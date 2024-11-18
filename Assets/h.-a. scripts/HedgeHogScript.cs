using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeHogScript : MonoBehaviour
{
    void OnEnable()
    {
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
