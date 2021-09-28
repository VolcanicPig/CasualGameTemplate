using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AutomatedMovement : MonoBehaviour
    {
        public static Action<AutomatedMovement> OnReachedDestination;


        [SerializeField] private float moveSpeed, minDistanceToDestination;

        [Header("Components")]
        [SerializeField] PlayerMovementAnimations _animations;

        private Vector3 _currentDestination;
        private bool _reachedDestination;

        private void Start()
        {
            _animations = GetComponent<PlayerMovementAnimations>();
        }

        private void FixedUpdate()
        {
            if (!_reachedDestination)
                MoveToDestination();
        }

        private void MoveToDestination()
        {
            if (DistanceToDestination() > minDistanceToDestination)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _currentDestination, step);
                _animations.SetRunning(true);
            }
            else
            {
                _reachedDestination = true;
                _animations.SetRunning(false);
                OnReachedDestination?.Invoke(this);
            }
        }

        public void SetDestination(Vector3 destination)
        {
            _currentDestination = destination;
            _reachedDestination = false;
        }

        private float DistanceToDestination()
        {
            return Vector3.Distance(transform.position, _currentDestination);
        }
    }
}
