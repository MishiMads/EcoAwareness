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

  public GameObject BeeSeed;
  public GameObject ButteflySeed;
  public GameObject HedgeHogSeed;
  
  public GameObject backgroundNoise;
  public GameObject BeeFuture;
  public GameObject BFFuture;
  public GameObject HedgeHogFuture;
  public GameObject BeeParticle;
  public GameObject BfParticle;
  public GameObject HedgeHogParticle;
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
      if(BeeParticle!=null)
      {
        BeeParticle.gameObject.SetActive(false);
      }
    }
    
    if (QuestManager.Instance.BFQuestComplete)
    {
      Instantiate(BFPlantPrefab , QuestManager.Instance.BFPlant, Quaternion.identity);
      BFFuture.gameObject.SetActive(true);
      if(BeeParticle!=null)
      {
        BfParticle.gameObject.SetActive(false);
      }
    }
    
    if (QuestManager.Instance.HedgeHogQuestComplete)
    {
      Instantiate(HedgeHogPlantPrefab , QuestManager.Instance.HedgeHogPlant, Quaternion.identity);
      HedgeHogFuture.gameObject.SetActive(true);
      if (HedgeHogParticle!=null)
      {
        HedgeHogParticle.gameObject.SetActive(false);
      }
    }

    if (QuestManager.Instance.beeQuestComplete || QuestManager.Instance.BFQuestComplete ||
        QuestManager.Instance.HedgeHogQuestComplete)
    {
      backgroundNoise.gameObject.SetActive(true);
    }
    
    if (Portal != null && QuestManager.Instance.HedgeHogQuestComplete && QuestManager.Instance.BFQuestComplete &&
        QuestManager.Instance.beeQuestComplete)
    {
      StartCoroutine(portalWait());
    }

    if (QuestManager.Instance.HasInteractedWith("hedgehog") && HedgeHogParticle != null)
    {
      Destroy(HedgeHogParticle);
    }
    if (QuestManager.Instance.HasInteractedWith("bee") && BeeParticle != null)
    {
      Destroy(BeeParticle);
    }
    
    if (QuestManager.Instance.HasInteractedWith("bf") && BfParticle != null)
    {
     Destroy(BfParticle);
    }

    StartCoroutine(removeSeed());

  }

  private IEnumerator portalWait()
  {
    yield return new WaitForSeconds(10f);
    Portal.gameObject.SetActive(true);

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
