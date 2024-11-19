using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This is a class that holds a tag and the associated prefab
[System.Serializable] //making it serialized allows the class to show up in the inspector
public class InteracttionBool
{
    public string Animal;
    public bool HasInteracted;
}

public class QuestManager : MonoBehaviour
{
   public static QuestManager Instance;
   public List<InteracttionBool> BoolList;
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
