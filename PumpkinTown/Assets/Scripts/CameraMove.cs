using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static bool startTime = false;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("MainCamera").transform.position = transform.position + new Vector3(3, 15, -5);
            GameObject.Find("FenceClose").transform.position = transform.position + new Vector3(1, -1, 0);
            GameObject.Find("EnemyCollider2").transform.position = transform.position + new Vector3(-50, 0, 0);
            GameObject.Find("EnemyCollider3").transform.position = transform.position + new Vector3(-50, 0, 0);
            GameObject.Find("EnemyCollider4").transform.position = transform.position + new Vector3(-50, 0, 0);
            GameObject.Find("EnemyCollider5").transform.position = transform.position + new Vector3(-50, 0, 0);
            startTime = true;
        }        
    }
}
