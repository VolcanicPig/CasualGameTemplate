using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using VolcanicPig.Mobile;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    public class PlayerTopDownMovement : PlayerMovement
    {
        [SerializeField] private float rotationSpeed;


        public override void Update()
        {
            base.Update();
            RotateTowardsMovement();
        }

        public override void HandleMovement()
        {
            Vector2 axis = _gestures.Axis / 2;

            if (axis == Vector2.zero)
            {
                _rb.angularVelocity = Vector3.zero;
                return;
            }
            
            transform.position += new Vector3(axis.x, 0, axis.y) * forwardsSpeed * Time.deltaTime;
        }

        private void RotateTowardsMovement()
        {
            Vector2 axis = _gestures.Axis / 2;
            Vector3 movementDirection = new Vector3(axis.x, 0, axis.y);

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
