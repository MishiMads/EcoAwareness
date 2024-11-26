using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestDialogue : MonoBehaviour
{
    // Start is called before the first frame update

    public openSpeechbouble hedgehogDialogue;
    public openSpeechbouble BeeDialogue;
    public openSpeechbouble BFDialogue;

    public string[] HedgeHogGratitude;
    public string[] BeeGratitude;
    public string[] BFGratiude;
    
    private void OnEnable()
    {
        // Subscribe to the SceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      DialogueUpdate();
    }

    public void DialogueUpdate()
    {
        if (QuestManager.Instance.BFQuestComplete)
        {
            BFDialogue.Replilk = BFGratiude;
        }

        if (QuestManager.Instance.HedgeHogQuestComplete)
        {
            hedgehogDialogue.Replilk = HedgeHogGratitude;
        }

        if (QuestManager.Instance.beeQuestComplete)
        {
            BeeDialogue.Replilk = BeeGratitude;
        }
    }
}
