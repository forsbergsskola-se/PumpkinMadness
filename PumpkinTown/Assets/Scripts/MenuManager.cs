using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject GameStartPanel, _exitButton;
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
        GameStartPanel.SetActive(state == GameState.StartGame);
    }

    void Start()
    {
        
    }

   
}
