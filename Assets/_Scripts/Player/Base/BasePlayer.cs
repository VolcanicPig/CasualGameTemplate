using System;
using UnityEngine;
using VolcanicPig.Mobile;

namespace Game
{
    public enum PlayerState
    {
        Moving,
        Activity,
        Fighting,
        End
    }
    
    public abstract class BasePlayer : MonoBehaviour
    {
        public static Action<PlayerState> PlayerStateChanged;

        private PlayerState _state;
        public PlayerState State
        { 
            get => _state;
            private set
            {
                _state = value;
                PlayerStateChanged?.Invoke(value);
            }
        }
        
        protected PlayerMovement movement;

        protected virtual void OnEnable()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged; 
        }

        protected virtual void OnDisable()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }
        
        protected virtual void Awake()
        {
            movement = GetComponent<PlayerMovement>();
        }
        
        protected virtual void OnGameStateChanged(GameState state)
        {
            if (state == GameState.InGame)
            {
                if(movement) movement.SetMovementEnabled(true);
            }
            else
            {
                if(movement) movement.SetMovementEnabled(false);
            }
        }
        
        public void SetState(PlayerState state)
        {
            State = state;

            switch (state)
            {
                case PlayerState.Moving:
                    movement.SetMovementEnabled(true);
                    break;
                
                case PlayerState.Activity:
                case PlayerState.End: 
                    movement.SetMovementEnabled(false);
                    break;
            }
        }
    }
}