using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAnimation : MonoBehaviour
{

    public openSpeechbouble speechManager;
    private float  StartPosition;

    //how often you want it to switch between going up and down
    public float switchInterval = 0.5f;
    //How fast you want it to go up and down
    public float speed = 1f;
    //timer used to check how much time has passed
    private float timer = 0.0f;
    //This bool is used to check weather the bee should move up or down
    private bool movingUp = true;
    // Start is called before the first frame update
    void Start()
    {
        //this stores the height the bee starts floating at
        StartPosition = transform.localPosition.y;;
    }

    // Update is called once per frame
    void Update()
    {
        
        // this moves the bee towards the starting height
        if (speechManager.IsTalking == false)
        {
            Vector3 targetPosition = new Vector3(transform.localPosition.x, StartPosition, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
        }
        
        else
        {
            AnimateFloating();
        }
    }

    //this function moves the butterfly up and down
    private void AnimateFloating()
    {
        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            movingUp = !movingUp;
            timer = 0.0f;
        } 
        //this is bassicly just a shorter version of an if statement, depeding one weather movingUp is true or false
        //the direction value will be positive or negative
        float direction = movingUp ? 1.0f : -1.0f;
        //this moves the be up at speed equal to the direction, then
        transform.position += Vector3.up * (direction * speed * Time.deltaTime);
    }
}
