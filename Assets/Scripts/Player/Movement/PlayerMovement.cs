using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool IsMoving => _isMoving;
        
        
        [SerializeField] protected float deltaMultiplier, forwardsSpeed, sideSpeed, minYRot, maxYRot;

        protected GestureController _gestures; 
        protected Player _player;
        protected Rigidbody _rb;

        protected bool _canMoveForwards;
        protected bool _canMoveSideways;
        protected bool _automatedMovementActive;
        protected bool _isMoving; 

        private void Start()
        {
            _gestures = GestureController.Instance; 
            _rb = GetComponent<Rigidbody>();
            _player = GetComponent<Player>();
        }

        public void SetMovementEnabled(bool enabled)
        {
            _canMoveForwards = enabled;
            _canMoveSideways = enabled;
        }

        public virtual void Update()
        {
            if (_player.State != PlayerState.Moving)
            {
                _rb.angularVelocity = Vector3.zero;
                return;
            }

            HandleMovement();
        }

        private float _yRot = 0;

        public virtual void HandleMovement()
        {
            if (_automatedMovementActive) return;

            if (_canMoveSideways)
            {
                Vector2 axis = _gestures.ScaledTouchDelta * deltaMultiplier;
                _yRot += axis.x * sideSpeed * Time.deltaTime;
                _yRot = Mathf.Clamp(_yRot, minYRot, maxYRot);

                transform.rotation = Quaternion.Euler(0, _yRot, 0);
            }
            else
            {
                if(_rb) _rb.angularVelocity = Vector3.zero;
            }

            if (_canMoveForwards)
            {
                transform.position += transform.forward * forwardsSpeed * Time.deltaTime;
                _isMoving = true; 
            }
            else
            {
                _isMoving = false;  
            }
        }

        public void AutomatedMovementToPosition(Transform target, Action onComplete)
        {
            _automatedMovementActive = true;
            _yRot = 0;
            StartCoroutine(CoAutomatedMovement(target, onComplete));
        }

        private IEnumerator CoAutomatedMovement(Transform target, Action onComplete)
        {
            while (transform.position != target.position)
            {
                float step = forwardsSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                transform.LookAt(target);
                yield return null;
            }

            transform.position = target.position;
            transform.rotation = target.rotation; 
            
            _automatedMovementActive = false;
            if (onComplete != null) onComplete?.Invoke();
        }
    }
}
