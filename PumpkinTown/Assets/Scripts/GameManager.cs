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

   public void StartGame()
   {
      StartCoroutine(LoadYourAsyncScene());
   }

   IEnumerator LoadYourAsyncScene()
   {
      // The Application loads the Scene in the background as the current Scene runs.
      // This is particularly good for creating loading screens.
      // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
      // a sceneBuildIndex of 1 as shown in Build Settings.

      AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");

      // Wait until the asynchronous scene fully loads
      while (!asyncLoad.isDone)
      {
         yield return null;
      }
      GameObject UI = GameObject.FindGameObjectWithTag("UI");
      GameObject gameOver = Instantiate(_gameOver, UI.transform);
      gameOver.transform.SetParent(UI.transform);
      _gameOver = gameOver;
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
