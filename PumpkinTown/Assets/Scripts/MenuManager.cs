using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    private GameManager _gameManager;
    
    
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _gameManager.UpdateGameState(GameState.MainMenu);
    }

    public void StartGame()
    {
        _gameManager.StartGame();
    }

    public void EndGame()
    {
        Application.Quit();
    }
    
}
