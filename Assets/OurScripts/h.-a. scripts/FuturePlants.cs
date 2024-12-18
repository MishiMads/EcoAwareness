using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuturePlants : MonoBehaviour
{
  //some functions in this script have been taken from other scripts that also used Onsceneload.
  //It seemed that having multiple scripts which used that functionality caused problems, with some of them not triggering
  //properly
  
  //these are the plants that are instatiated in each scene when the scene is loaded, in the future scene its fully grown plants in the pressent its sprouts
  public GameObject BeePlantPrefab;
  public GameObject BFPlantPrefab;
  public GameObject HedgeHogPlantPrefab;
  //these are the seeds the player uses. They are here so they can be set as not active if they have already been used.
  //however this functionality dosnt seem to work when the player travels to the present scene
  public GameObject BeeSeed;
  public GameObject ButteflySeed;
  public GameObject HedgeHogSeed;
  
  public GameObject backgroundNoise; //the bird noises that can be heard in the future
  
  //plants and animals in the future scene which will be set as active upon completion of a quest, locations are predetirmened
  public GameObject BeeFuture;
  public GameObject BFFuture;
  public GameObject HedgeHogFuture;
  
  //the particle system attached to the animals, which are used to highlight the animals. These are here so they can be
  //disabled if the animal has already been interacted with or the quest is already completed
  public GameObject BeeParticle;
  public GameObject BfParticle;
  public GameObject HedgeHogParticle;
  
  //the portal which is used in the future to exit the game
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
    //check if the animals quest has been completed, then plants the associated plant, activates the props and
    //deactivates the particles system
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

    //activates the bird noises if any of the quests have been completed
    if (QuestManager.Instance.beeQuestComplete || QuestManager.Instance.BFQuestComplete ||
        QuestManager.Instance.HedgeHogQuestComplete)
    {
      backgroundNoise.gameObject.SetActive(true);
    }
    
    //this checks if all the portals have been completed and then activates the corutine which activates the portal
    //In hindsight checking if the portal is null is uneccessary as setting null as active hasnt caused
    //problems elsewhere
    if (Portal != null && QuestManager.Instance.HedgeHogQuestComplete && QuestManager.Instance.BFQuestComplete &&
        QuestManager.Instance.beeQuestComplete)
    {
      StartCoroutine(portalWait());
    }

    //these destroy the animal particles if the animal has been interacted with.
    //it is set to destroy because in the original script when it was set to just disable it didnt seem to work
    //So it was set to destroy instead to see if that would have an effect, it did not. Then the functionality from that
    //was moved in here instead. It dosnt actualy matter weather the particals are destroyed or disabled as nothing tries
    //to set them active again.
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
    //this corutine waits for 1 second before checking the status of the quests and then setting the associated seeds as
    //in active. This was als originally in a seperate script. The seeds being deactivated didnt seem to habben so the 
    //functionality was moved into this script, which still didnt fix the issue. Then the though was that something was
    //turning the seeds on again, so i implemented a delay to see if that would have an effect, it did not. As of now
    //the seed get turned off in the future scene but not in the pressent scene. I dont know why.
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
