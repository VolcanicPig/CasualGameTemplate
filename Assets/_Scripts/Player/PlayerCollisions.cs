using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Collectable;

namespace Game
{
    public class PlayerCollisions : MonoBehaviour
    {
        private Player _player;
        private PlayerMovement _movement;
		
        private const string _kEnd = "End";
        private const string _kCollectable = "Collectable"; 

        private void Start()
        {
            _player = GetComponent<Player>(); 
            _movement = GetComponent<PlayerMovement>();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag(_kEnd))
            {
                GameManager.Instance.EndGame(true);
            }

            if (other.CompareTag(_kCollectable))
            {
                Collectable collectable = other.GetComponent<Collectable>(); 
                
                if(collectable)
                    collectable.Collect();
            }
        }
    }
}