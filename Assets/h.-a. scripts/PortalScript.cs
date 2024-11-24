using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
 private int EndSceneIndex = 3;


 private void OnTriggerEnter(Collider other)
 {
  if (other.gameObject.CompareTag("Player"))
  {
   Debug.Log("Portal collision");
   SceneManager.LoadScene(sceneBuildIndex: EndSceneIndex);
  }
 }
}
