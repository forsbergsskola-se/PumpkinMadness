using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject GameStartPanel, _exitButton;
    [SerializeField] private TextMeshProUGUI _stateText;
    private GameManager _gameManager;
    private void GameManagerOnGameStateChanged(GameState state)
    {
        GameStartPanel.SetActive(state == GameState.StartGame);
    }

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

   
}
