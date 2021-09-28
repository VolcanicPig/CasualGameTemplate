using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float forwardForce;
        [SerializeField] private bool localForward;

        private Vector3 _positionLastFrame;
        private Vector3 _fakeVelocity;
        public Vector3 FakeVelocity => _fakeVelocity;
        private bool _canMove = false;
        public bool CanMove => _canMove;

        public virtual void FixedUpdate()
        {
            ForwardsMovement();
            _fakeVelocity = CalculateFakeVelocity();
        }

        private void ForwardsMovement()
        {
            if (!_canMove) return;

            if (localForward)
                transform.position += transform.forward * forwardForce * Time.deltaTime;
            else
                transform.position += Vector3.forward * forwardForce * Time.deltaTime;
        }

        private Vector3 CalculateFakeVelocity()
        {
            if (!_canMove) return Vector3.zero;

            Vector3 position = transform.position;
            Vector3 velocity = position - _positionLastFrame;
            _positionLastFrame = position;
            return velocity;
        }

        public void SnapPlayersPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetCanMove(bool canMove) => _canMove = canMove;
    }
}
