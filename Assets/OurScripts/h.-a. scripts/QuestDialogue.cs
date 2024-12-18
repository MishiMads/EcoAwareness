using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestDialogue : MonoBehaviour
{
    // Start is called before the first frame update

    //The open speach bubble scripts for the various scripts
    public openSpeechbouble hedgehogDialogue;
    public openSpeechbouble BeeDialogue;
    public openSpeechbouble BFDialogue;

    //new set of lines for the animals for after their quest is completed, aside from the bee they are blank, which 
    //results in the animals not showing a speechbubble. The bee has the name Jimmy from when i was testing if it worked
    //i forgot to take that out when we made the final build and since the text wasnt resized to fit the scale in the future
    // and present scene the the only text that ends up being visible in the bees post quest speechbubble is a J
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
    
    //this updates the dialogue of the animals when their quest has been completed
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
