using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFAnimation : MonoBehaviour
{

    public Animator BfAnimator;
    public float WalkingAnimationSpeed;
    public float TalkingAnimationSpeed;
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
           //we will be changing speed here
           BfAnimator.SetFloat("speed",TalkingAnimationSpeed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
            BfAnimator.SetFloat("speed",WalkingAnimationSpeed);
        }
    }
}
