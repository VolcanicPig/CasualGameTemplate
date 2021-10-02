using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerSlideMovement : PlayerMovement
    {
        public override void HandleMovement()
        {
            if (_automatedMovementActive) return;

            Vector3 cachedPosition = transform.position;
            Vector2 touchDelta = _gestures.TouchDelta;

            if (_canMoveSideways)
            {
                cachedPosition.x += touchDelta.x * sideSpeed * Time.deltaTime;
                cachedPosition.x = Mathf.Clamp(cachedPosition.x, -5, 5);
            }

            if (_canMoveForwards)
            {
                cachedPosition.z += forwardsSpeed * Time.deltaTime;
                _isMoving = true;
            }
            else
            {
                _isMoving = false; 
            }

            float step = sideSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, cachedPosition, step);
        }
    }
}
