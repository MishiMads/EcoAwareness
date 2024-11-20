using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogAnimation : MonoBehaviour
{
    public Animator hedgehogAnim;

    public void Start()
    {
        hedgehogAnim.SetBool("IsWalking",true);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
           hedgehogAnim.SetBool("IsWalking",false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
            hedgehogAnim.SetBool("IsWalking",true);
        }
    }


}
