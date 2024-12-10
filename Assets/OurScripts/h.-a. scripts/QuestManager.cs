using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This is a class that holds a name and a bool, its used to check
[System.Serializable] //making it serialized allows the class to show up in the inspector
public class InteracttionBool
{
    public string Animal;
    public bool HasInteracted;
}

public class QuestManager : MonoBehaviour
{
   public static QuestManager Instance;
   public List<InteracttionBool> BoolList;//lists that contains the animals and weather the players has interacted with them
   private Dictionary<string, bool> interactionLookup; //dictionary thats used to check parts of the interaction list
   //should have properly just had a Dictionary from the start instead of jurry rigging it into the list
   
   //weather the quests have been completed
   public bool beeQuestComplete = false;
   public bool BFQuestComplete = false;
   public bool HedgeHogQuestComplete = false;
   
   //the locations that plants are placed at
   public Vector3 BeePlant;
   public Vector3 BFPlant;
   public Vector3 HedgeHogPlant;

   //the location of the player when scene transition happens
   public Vector3 PlayerLocation;
   
   //this makes sure that there is only one instance of this script in any given scene and makes it so the script wont
   //destroy itself on load. If there is another instance of this script when it awakes, it destroys itself, as the oldest
   //script is already awake when a new scene is loaded, the data saved here will be preserved.
   private void Awake(){
      if (Instance != null && Instance!=this)
      {
        Destroy(gameObject);
      }
      else
      {
          Instance=this;
          DontDestroyOnLoad(gameObject); 
          //this create a dictionary bassed on BoolList which tracks who the player has interacted with
          //this is because normally a forloop is used to look up data in that list and the dictionary method is faster
          //in hindsight i should just have made the whole thing a Dictionary from the start.
          interactionLookup = new Dictionary<string, bool>();
          foreach (var interaction in BoolList)
          {
              interactionLookup[interaction.Animal] = interaction.HasInteracted;
          }
      }
  
   }
   
   //public function used in other scripts to check weather or not an animal has been interacted with
   public bool HasInteractedWith(string animal)
   {
       return interactionLookup.ContainsKey(animal) && interactionLookup[animal];
   }
   
   //public function that sets an animal as having been interacted with. In contrast to the dictionary its to complicated
   //should have just made the whole thing a dictionary
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
