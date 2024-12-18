using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
 private int EndSceneIndex = 3;
    
//this script simply sends the player to the ending scene when they enter a trigger collider
 private void OnTriggerEnter(Collider other)
 {
        Debug.Log("The Portal has detected a collision");

        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log("Portal collision with player detected.");
            SceneManager.LoadScene(sceneBuildIndex: EndSceneIndex);
        }
 }
}
