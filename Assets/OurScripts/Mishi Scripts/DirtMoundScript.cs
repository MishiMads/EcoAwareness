using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtMoundScript : MonoBehaviour
{
    [SerializeField] public int sproutVar;

    public GameObject sproutBee;
    public GameObject sproutBF;
    public GameObject sproutHedgehog;
    // First of all, this method checks if a seed has been planted (if the Mound GameObject is active or not) and then checks if which seed is planted.
    // This is dependent on, which tag the seed has and leads to the sprout variable (sproutVar) getting one of 3 variables. 
    public void OnTriggerEnter(Collider other)
    {
        if (sproutBee.activeSelf == false && sproutBF.activeSelf == false && sproutHedgehog.activeSelf == false)
        {
            if (other.CompareTag("ButterflySeed"))
            {
                sproutVar = 1;
                sproutBF.SetActive(true);
                Destroy(other.gameObject);
            }
            
            else if (other.CompareTag("BeeSeed"))
            {
                sproutVar = 2;
                sproutBee.SetActive(true);
                Destroy(other.gameObject);
            }
            
            else if (other.CompareTag("HedgehogSeed"))
            {
                sproutVar = 3;
                sproutHedgehog.SetActive(true);
                Destroy(other.gameObject);
            }
        }
    }
}