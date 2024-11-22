using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuturePlants : MonoBehaviour
{
  public GameObject BeePlantPrefab;
  public GameObject BFPlantPrefab;
  public GameObject HedgeHogPlantPrefab;
  
  
  public GameObject BeeFuture;
  public GameObject BFFuture;
  public GameObject HedgeHogFuture;

  public GameObject Portal;
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
      BeeFuture.gameObject.SetActive(true);
    }
    
    if (QuestManager.Instance.BFQuestComplete)
    {
      Instantiate(BFPlantPrefab , QuestManager.Instance.BFPlant, Quaternion.identity);
      BFFuture.gameObject.SetActive(true);
    }
    
    if (QuestManager.Instance.HedgeHogQuestComplete)
    {
      Instantiate(HedgeHogPlantPrefab , QuestManager.Instance.HedgeHogPlant, Quaternion.identity);
      HedgeHogFuture.gameObject.SetActive(true);
      
    }

    if (Portal != null && QuestManager.Instance.HedgeHogQuestComplete && QuestManager.Instance.BFQuestComplete &&
        QuestManager.Instance.beeQuestComplete)
    {
      Portal.gameObject.SetActive(true);
    }
  }
}
