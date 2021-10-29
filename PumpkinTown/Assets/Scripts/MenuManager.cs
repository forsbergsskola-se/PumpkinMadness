using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameStartPanel, _exitButton;
    [SerializeField] private TextMeshProUGUI _stateText;

    private void Awake()
    {
        GameManager.OneGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OneGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        _gameStartPanel.SetActive(state == GameState.StartGame);
    }

    void Start()
    {
        
    }

   
}
