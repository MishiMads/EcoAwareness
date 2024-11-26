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
            QuestManager.Instance.SetInteractedWith(ThisAnimal, true);
        }
    }
 

    public void CheckParticles()
    {
        
        Debug.Log(ThisAnimal+" Is checking if it has iteracted");
        //looks through the list of animals in questManager
        if (QuestManager.Instance.HasInteractedWith(ThisAnimal))
        {
            Debug.Log(ThisAnimal+" Has interacted");
            //disables the particle system
            ParticleEffects.gameObject.SetActive(false);
        }
        
    }
            
        
    
}
