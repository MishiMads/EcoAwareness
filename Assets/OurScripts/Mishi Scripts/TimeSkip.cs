using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.UIElements;


public class TimeSkip : MonoBehaviour
{
    public int sproutVar;
    public List<GameObject> Plants;
    public GameObject Mound;
    public GameObject Soil;

    
    // The "timeskip" method checks what the sproutVar is (which indicates which seed has been planted)
    // and activates the corresponding GameObject
    public void timeskip()
    {
        Planting planting = Soil.GetComponent<Planting>();
        sproutVar = planting.sproutVar;
        
        if (sproutVar == 1)
        {
            Mound.SetActive(false);
            Plants[0].SetActive(true);
        }
        
        if (sproutVar == 2)
        {
            Mound.SetActive(false);
            Plants[1].SetActive(true);
        }
        
        if (sproutVar == 3)
        {
            Mound.SetActive(false);
            Plants[2].SetActive(true);
        }
        
    }
}
