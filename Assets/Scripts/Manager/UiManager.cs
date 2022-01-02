using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile;
using VolcanicPig.Mobile.Ui;

namespace Game
{
    public class UiManager : UiManagerTemplate<UiManager>
    {
        [SerializeField] private GameObject confetti; 
        
        private void OnEnable()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged; 
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    OpenMenu("Start");
                    break;
                case GameState.InGame:
                    OpenMenu("InGame");
                    break;
                case GameState.End:
                    OpenMenu("End");
                    break;
            }

            confetti.SetActive(state == GameState.End && GameManager.Instance.GetWinState == WinState.Win);
        }
    }
}
