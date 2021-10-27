using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            ScoreScript.scoreValue += 1;
            Debug.Log("Object destroyd");
        }


    }
}

