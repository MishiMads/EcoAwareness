using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
   public static QuestManager Instance;
   
   public bool beeQuestComplete = false;
   public bool BFQuestComplete = false;
   public bool HedgeHogQuestComplete = false;
   
   public Vector3 BeePlant;
   public Vector3 BFPlant;
   public Vector3 HedgeHogPlant;
   
   private void Awake(){
      if (Instance != null && Instance!=this)
      {
        Destroy(gameObject);
      }
      else
      {
          Instance=this;
          DontDestroyOnLoad(gameObject); 
      }
  
   }
}
