using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeedManager : MonoBehaviour
{

    public GameObject BeeSeed;
    public GameObject ButteflySeed;
    public GameObject HedgeHogSeed;
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

        StartCoroutine(removeSeed());
    }


    private IEnumerator removeSeed()
    {
        
        yield return new WaitForSeconds(1);

        
        if (QuestManager.Instance.beeQuestComplete)
        {
            BeeSeed.gameObject.SetActive(false);
        }

        if (QuestManager.Instance.BFQuestComplete)
        {
            ButteflySeed.gameObject.SetActive(false);
        }

        if (QuestManager.Instance.HedgeHogQuestComplete)
        {
            HedgeHogSeed.gameObject.SetActive(false);
        }

        Debug.Log("seeds removed");
    }
}
