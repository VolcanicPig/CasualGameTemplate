using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerJoystickMovement : BasePlayerMovement
    {
        private Joystick _joystick;

        protected override void Awake()
        {
            base.Awake();
            _joystick = FindObjectOfType<Joystick>(); 
        }

        
        public void Update()
        {
            if (player.State != PlayerState.Moving)
            {
                rb.angularVelocity = Vector3.zero;
                return;
            }

            if (canMoveForwards)
            {
                HandleMovement();	
            }
        }
        
        public void HandleMovement()
        {
            Vector3 move = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);

            Transform cameraTransform = Helpers.Camera.transform;
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward = camForward.normalized;
            camRight = camRight.normalized;
			
            transform.position += (camForward * move.z + camRight * move.x) * forwardsSpeed * Time.deltaTime;
			
            if (move != Vector3.zero)
            {
                IsMoving = true;
                transform.forward = (camForward * move.z + camRight * move.x);
            }
            else
            {
                IsMoving = false; 
            }
        }
    }
}
