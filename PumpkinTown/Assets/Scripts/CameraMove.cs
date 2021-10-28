using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{       
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("MainCamera").transform.position = transform.position + new Vector3(3, 15, -5);
            GameObject.Find("FenceClose").transform.position = transform.position + new Vector3(1, -1, 0);            
        }        
    }
}