using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPumpkin : MonoBehaviour
{
   

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Pumpkin")
        {
            // to use GetComponent, the Enemy class (the bit inside the < >) has to
            // inherit from monobehaviour - more on that below
            // I've changed it to be Timer instead of Enemy, so it is more clear what the thing is doing!
            Timer timer = col.gameObject.GetComponent<Timer>();
            timer.counter += Time.deltaTime;
            // here it's _always_ destroying the gameobject
            // you need an "if" here to check if the counter has reached a certain amount of time
            if (timer.counter >= 2f) // if the counter is greater than 2 seconds
                Destroy(gameObject);
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Pumpkin")
        {
            Timer timer = col.gameObject.GetComponent<Timer>();
            timer.counter = 0.0f;
        }
    }
}

// the "MonoBehaviour" bit here means it can be attached to an object in the scene. 
// Attach this to the things you've tagged "Player" and "Pumpkin"
// It will also have to be in a separate file called "Timer.cs"
