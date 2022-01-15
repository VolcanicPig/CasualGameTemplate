using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    public class PlayerMovement : BasePlayerMovement
    {
        [SerializeField] protected float deltaMultiplier, sideSpeed, minYRot, maxYRot;
        
        public void Update()
        {
            if (player.State != PlayerState.Moving)
            {
                rb.angularVelocity = Vector3.zero;
                return;
            }

            HandleMovement();
        }

        private float _yRot = 0;
        public void HandleMovement()
        {
            if (automatedMovementActive) return;

            if (canMoveSideways)
            {
                Vector2 axis = gestures.ScaledTouchDelta * deltaMultiplier;
                _yRot += axis.x * sideSpeed * Time.deltaTime;
                _yRot = Mathf.Clamp(_yRot, minYRot, maxYRot);

                transform.rotation = Quaternion.Euler(0, _yRot, 0);
            }
            else
            {
                if(rb) rb.angularVelocity = Vector3.zero;
            }

            if (canMoveForwards)
            {
                transform.position += transform.forward * forwardsSpeed * Time.deltaTime;
                IsMoving = true; 
            }
            else
            {
                IsMoving = false;  
            }
        }
    }
}
