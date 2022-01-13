using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    public abstract class BasePlayerMovement : MonoBehaviour
    {
        [SerializeField] protected float forwardsSpeed; 
        
        public bool IsMoving => isMoving; 
        
        protected bool isMoving;
        protected bool automatedMovementActive;
        protected bool canMoveForwards;
        protected bool canMoveSideways;
        
        protected GestureController gestures; 
        protected BasePlayer player;
        protected Rigidbody rb;


        protected virtual void Awake()
        {
            gestures = GestureController.Instance; 
            rb = GetComponent<Rigidbody>();
            player = GetComponent<BasePlayer>(); 
        }
        
        public void SetMovementEnabled(bool enabled)
        {
            canMoveForwards = enabled;
            canMoveSideways = enabled;
        }
        
        public void AutomatedMovementToPosition(Transform target, Action onComplete)
        {
            automatedMovementActive = true;
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
            
            automatedMovementActive = false;
            if (onComplete != null) onComplete?.Invoke();
        }
    }
}