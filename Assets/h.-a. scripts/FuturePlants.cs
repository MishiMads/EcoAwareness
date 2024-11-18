using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuturePlants : MonoBehaviour
{
  public GameObject BeePlantPrefab;
  public GameObject BFPlantPrefab;
  public GameObject HedgeHogPlantPrefab;
  
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
    if (QuestManager.Instance.beeQuestComplete)
    {
      Instantiate(BeePlantPrefab , QuestManager.Instance.BeePlant, Quaternion.identity);
    }
    
    if (QuestManager.Instance.BFQuestComplete)
    {
      Instantiate(BFPlantPrefab , QuestManager.Instance.BFPlant, Quaternion.identity);
    }
    
    if (QuestManager.Instance.HedgeHogQuestComplete)
    {
      Instantiate(HedgeHogPlantPrefab , QuestManager.Instance.HedgeHogPlant, Quaternion.identity);
    }
  }
}
