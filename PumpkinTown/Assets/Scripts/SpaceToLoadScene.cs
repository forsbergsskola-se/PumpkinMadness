using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceToLoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string MainScene;
    public float timeLength = 10f; //seconds
    private float elapsedTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("MainScene");

        //elapsedTime += Time.deltaTime;
        //if (elapsedTime > timeLength)
          //  SceneManager.LoadScene(MainScene);
    }
}