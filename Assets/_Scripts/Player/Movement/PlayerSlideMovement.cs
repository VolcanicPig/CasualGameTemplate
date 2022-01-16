using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    public class PlayerSlideMovement : BasePlayerMovement
    {
        public float CurrentSpeed { get; private set; }
        
        [SerializeField] protected float deltaMultiplier, topSpeed, topSpeedLeeway, 
            acceleration, deceleration, rotateSpeed, minYRot, maxYRot;

        protected override void Update()
        {
            if (player.State != PlayerState.Moving)
            {
                rb.angularVelocity = Vector3.zero;
                return;
            }
            
            base.Update();

            HandleMovement();
            HandleRotation();
        }

        private float _yRot = 0;
        public void HandleMovement()
        {
            if (!canMoveForwards)
            {
                IsMoving = false; 
                return;
            }
            
            if (automatedMovementActive) return;

            if (CurrentSpeed < (topSpeed - topSpeedLeeway) || CurrentSpeed > (topSpeed + topSpeedLeeway))
            {
                float step = CurrentSpeed > topSpeed ? deceleration : acceleration; 
                CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, topSpeed, step * Time.deltaTime); 
            }

            transform.position += transform.forward * CurrentSpeed * Time.deltaTime;
            IsMoving = true; 
        }
        
        private void HandleRotation()
        {
            if (!canMoveSideways) return;

            Vector2 axis = gestures.ScaledTouchDelta * deltaMultiplier;
            if (gestures.Touching)
            {
                _yRot += axis.x * rotateSpeed * Time.deltaTime; 
                _yRot = Mathf.Clamp(_yRot, minYRot, maxYRot); 	
            }
            else
            {
                _yRot = Mathf.MoveTowards(_yRot, 0, 100 * Time.deltaTime);
            }

            transform.rotation = Quaternion.Euler(0, _yRot, 0); 
        }
    }
}
