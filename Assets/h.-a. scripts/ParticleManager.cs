using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleManager : MonoBehaviour
{
    //what animal this is
    public string ThisAnimal;

    public GameObject ParticleEffects;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            ParticleEffects.gameObject.SetActive(false);
            //finds this animal in the list of animals
            foreach (var InteracttionBool in QuestManager.Instance.BoolList)
            {
                if (ThisAnimal == InteracttionBool.Animal)
                {
                    //marks them as having been interacted with
                    InteracttionBool.HasInteracted = true;
                    // This ends the loop 
                    break;
                }
            
            }
        }
    }
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
        //looks through the list of animals in questManager
        foreach (var InteracttionBool in QuestManager.Instance.BoolList)
        {
            if (ThisAnimal == InteracttionBool.Animal)
            {
                //checks if the animal has already been talked too
                if (InteracttionBool.HasInteracted == true)
                {
                    Debug.Log(ThisAnimal+" Has interacted");
                    //disables the particle system
                    ParticleEffects.gameObject.SetActive(false);
                }
                // This ends the loop 
                break;
            }
            
        }
    }
}
