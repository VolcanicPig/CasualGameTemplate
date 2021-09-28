using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using VolcanicPig.Mobile;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    public class PlayerTopDownMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed, rotationSpeed;

        private bool _canMove = true;
        public bool CanMove => _canMove;

        private Vector3 _fakeVelocity;
        public Vector3 FakeVelocity => _fakeVelocity;

        private Vector3 _positionLastFrame;
        private GestureController _gestureController;
        private PlayerMovementAnimations _animations;
        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.InGame)
            {
                _canMove = true;
            }
            else if (state == GameState.End)
            {
                _canMove = false;
                _animations.SetRunning(false);
            }
        }

        private void Start()
        {
            _canMove = false;
            _gestureController = GestureController.Instance;
            _animations = GetComponent<PlayerMovementAnimations>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
            RotateTowardsMovement();

            _fakeVelocity = CalculateFakeVelocity();
        }

        private void HandleMovement()
        {
            if (!_canMove) return;
            if (!_gestureController) return;

            Vector2 axis = _gestureController.Axis / 2;

            if (axis == Vector2.zero)
            {
                _animations.SetRunning(false);
                _rigidbody.angularVelocity = Vector3.zero;
                return;
            }

            _animations.SetRunning(true);
            transform.position += new Vector3(axis.x, 0, axis.y) * moveSpeed * Time.deltaTime;
        }

        private void RotateTowardsMovement()
        {
            if (!_canMove) return;
            if (!_gestureController) return;
            Vector2 axis = _gestureController.Axis / 2;
            Vector3 movementDirection = new Vector3(axis.x, 0, axis.y);

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        private Vector3 CalculateFakeVelocity()
        {
            if (!_canMove) return Vector3.zero;

            Vector3 position = transform.position;
            Vector3 velocity = position - _positionLastFrame;
            _positionLastFrame = position;
            return velocity;
        }
    }
}
