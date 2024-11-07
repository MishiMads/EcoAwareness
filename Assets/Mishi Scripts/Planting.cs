using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planting : MonoBehaviour
{
    [SerializeField] public int sproutVar;

    public GameObject Mound;
    // First of all, this method checks if a seed has been planted (if the Mound GameObject is active or not) and then checks if which seed is planted.
    // This is dependent on, which tag the seed has and leads to the sprout variable (sproutVar) getting one of 3 variables. 
    private int OnTriggerEnter(Collider other)
    {
        if (Mound.activeSelf == false)
        {
            if (other.CompareTag("Seed1"))
            {
                sproutVar = 1;
                Mound.SetActive(true);
                Destroy(other.gameObject);
            }
            
            else if (other.CompareTag("Seed2"))
            {
                sproutVar = 2;
                Mound.SetActive(true);
                Destroy(other.gameObject);
            }
            
            else if (other.CompareTag("Seed3"))
            {
                sproutVar = 3;
                Mound.SetActive(true);
                Destroy(other.gameObject);
            }
        }

        return sproutVar;
    }
}
