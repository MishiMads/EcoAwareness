using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class patrol : MonoBehaviour
{
    public float speed = 2f;
    
    //the start position is the position of the npc when the game starts
    private Vector3 StartPosition;
    private bool moveforward = true;
    private bool move = true;
    private Vector3 goalPosition;
    //this gameobject is what the npc will walk toward from its start position
    public GameObject goal;
    void Start()
    {
        //recording positions. Note you cannot curently change what positions the npc walk between while the game is running
        StartPosition = transform.position;
        goalPosition = goal.transform.position;
        rotate(goalPosition);
    }

    void Update()
    {
        if (move)
        { 
            //here is where movement is handle using transform position
            switch (moveforward)
            {
                case true:
                    transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed * Time.deltaTime);
                    break;
                case false:
                    transform.position = Vector3.MoveTowards(transform.position, StartPosition, speed * Time.deltaTime);
                    break;
            }
            
            //when the npc reach their goal they turn around and walk back towards the start
            if (transform.position == goalPosition)
            {
                moveforward = false;
                rotate(StartPosition);
            }
               
            //when the npc reach the start position they turn around walk towards the goal
            if (transform.position == StartPosition)
            {
                moveforward = true;
                rotate(goalPosition);
            }

        }


    }
    //this function orients the npc in the direction of the designated transform
    private void rotate(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(-direction);
    }
  
    //this function makes the npc stop and look in a certain direction
    public void stopAndLook(GameObject look)
    {
        move = false;
        Vector3 lookPosition = look.transform.position;
        rotate(lookPosition);
    }

    //this function calls a corutine which waits for half a second before turning towards its curent patrol goal and resuming its patrol
    public void resume()
    {
        StartCoroutine(Resuming());
    }

    private IEnumerator Resuming()
    {
        yield return new WaitForSeconds(0.5f);
        switch (moveforward)
        {
            case true:
                rotate(goalPosition);
                break;
            case false:
                rotate(StartPosition);
                break;
            
        }

        move = true;
    }
}
    
    

