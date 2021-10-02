using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerCollisions : MonoBehaviour
    {
        private Player _player;
        private PlayerMovement _movement;
		
        private const string K_End = "End";

        private void Start()
        {
            _player = GetComponent<Player>(); 
            _movement = GetComponent<PlayerMovement>();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag(K_End))
            {
                GameManager.Instance.EndGame(true);
            }
        }
    }
}