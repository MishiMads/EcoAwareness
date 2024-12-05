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
   public List<InteracttionBool> BoolList; //this list is used to track weather an animal as been interacted with
   private Dictionary<string, bool> interactionLookup; //this dictionary is used to find and refernce animals in the bool  list
   
   //these bools track weather a player has planted the animals prefered plant
   public bool beeQuestComplete = false;
   public bool BFQuestComplete = false;
   public bool HedgeHogQuestComplete = false;
   
   //these are used to track the location at which the various plants where planted
   public Vector3 BeePlant;
   public Vector3 BFPlant;
   public Vector3 HedgeHogPlant;

   //this is used to track the location of the player between scene transitions, so that the player can arrive at the 
   //same location whenever they time travel.
   public Vector3 PlayerLocation;
   
   
   private void Awake(){
       //this checks if there are other instances of this script when it awakes, if there is it destroys itself
      if (Instance != null && Instance!=this)
      {
        Destroy(gameObject);
      }
      //If there isnt this script sets itself as the main instance to reference, and marks itself to not be destroyed 
      //when a new scene is loaded, it also sets the interactionLookup to match the interaction list.
      else
      {
          Instance=this;
          DontDestroyOnLoad(gameObject); 
          interactionLookup = new Dictionary<string, bool>();
          foreach (var interaction in BoolList)
          {
              interactionLookup[interaction.Animal] = interaction.HasInteracted;
          }
      }
  
   }
   
   //this function checks weather an animal has been interacted with, it can be refernced by other scripts.
   public bool HasInteractedWith(string animal)
   {
       return interactionLookup.ContainsKey(animal) && interactionLookup[animal];
   }
   
   //This function sets an animal as having been interacted with, it can be used be other scripts
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
