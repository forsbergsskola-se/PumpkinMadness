using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   private bool gameHasEnded = false;

   public float restartDelay = 2f;
   
   public void GameOver()
   {
      if (gameHasEnded == false)
      {
         gameHasEnded = true;
         Debug.Log("GAME OVER");
         Invoke("Restart", restartDelay);
      }
      
   }

   void Restart()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
