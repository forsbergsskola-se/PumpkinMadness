using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinScript : MonoBehaviour
{
    bool inside = false;
    bool startTime = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            startTime = true;
            
        //StartCoroutine(pumpkinTimer());
        //IEnumerator pumpkinTimer()
        //{
        //startTime = true;
        //yield return new WaitForSeconds(3);
        //Destroy(gameObject);
        //ScoreScript.scoreValue += 1;
        //inside = true;
        //Debug.Log("Object destroyd");
        //}
        //if (inside == false)
        //{
        //StopCoroutine(pumpkinTimer());
        //}
        
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
                Debug.Log("StartTimetrue");
                yield return new WaitForSeconds(3);
                Destroy(gameObject);
            }
        }
        if (startTime == false)
        {
            StopAllCoroutines();
            Debug.Log("Exit");
        }
    }
}

