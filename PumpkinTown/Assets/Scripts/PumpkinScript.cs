using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinScript : MonoBehaviour
{
    bool inside = false;
    bool startTime = false;
    int pumpkinScore;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            startTime = true;        
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        startTime = false;        
    }
    private void FixedUpdate()
    {
        if (startTime == true)
        {
            StartCoroutine(pumpkinTimer());
            IEnumerator pumpkinTimer()
            {                
                yield return new WaitForSeconds(3);
                pumpkinScore++;
                Destroy(gameObject);
            }
        }
        if (startTime == false)
        {
            StopAllCoroutines();            
        }
    }
}

