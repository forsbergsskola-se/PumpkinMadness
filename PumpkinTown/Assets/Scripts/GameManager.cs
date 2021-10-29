using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   public float restartDelay = 2f;
   public GameState State;

   [SerializeField] GameObject _gameOver;
   public static event Action<GameState> OneGameStateChanged; 

   private bool gameHasEnded = false;

   private void Awake()
   {
      Instance = this;
      DontDestroyOnLoad(this.gameObject);
   }

   private void Start()
   {
      _gameOver.SetActive(false);
      UpdateGameState(GameState.StartGame);
   }

   public void UpdateGameState(GameState newState)
   {
      State = newState;

      switch (newState)
      {
         case GameState.MainMenu:
            break;
         case GameState.StartGame:
            _gameOver.SetActive(false);
            HandleGameStart();
            break;
         case GameState.Play:
            break;
         case GameState.Victory:
            break;
         case GameState.Lose:
            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
      }

      OneGameStateChanged?.Invoke(newState);
   }

   private void HandleGameStart()
   {
     
   }

   public void GameOver()
   {
      if (gameHasEnded == false)
      {
         gameHasEnded = true;
         Debug.Log("GAME OVER");
         _gameOver.SetActive(true);
         Invoke("Restart", restartDelay);
      }
      
   }

   void Restart()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
   
  
}
public enum GameState 
{
   MainMenu,
   StartGame,
   Play,
   Victory,
   Lose
}
