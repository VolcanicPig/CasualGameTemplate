using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerAnimations : MonoBehaviour
    { 
        [Header("References")]
        [SerializeField] private Animator anim;

        private PlayerMovement _movement;
        
        private static readonly int Running = Animator.StringToHash("Running");

        private void Start()
        {
            _movement = GetComponent<PlayerMovement>(); 
        }

        private void Update()
        {
            anim.SetBool(Running, _movement.IsMoving);
        }
    }
}
