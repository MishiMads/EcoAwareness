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
   private Dictionary<string, bool> interactionLookup;
   
   public bool beeQuestComplete = false;
   public bool BFQuestComplete = false;
   public bool HedgeHogQuestComplete = false;
   
   public Vector3 BeePlant;
   public Vector3 BFPlant;
   public Vector3 HedgeHogPlant;

   public Vector3 PlayerLocation;
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
   
   public bool HasInteractedWith(string animal)
   {
       return interactionLookup.ContainsKey(animal) && interactionLookup[animal];
   }
   
   public void SetInteractedWith(string animal, bool hasInteracted)
   {
       if (interactionLookup.ContainsKey(animal))
       {
           interactionLookup[animal] = hasInteracted;

           // Also update the InteractionList for consistency if needed
           foreach (var interaction in BoolList)
           {
               if (interaction.Animal == animal)
               {
                   interaction.HasInteracted = hasInteracted;
                   break;
               }
           }
       }
   }
}
