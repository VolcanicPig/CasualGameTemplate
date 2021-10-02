using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    [RequireComponent(typeof(PlayerSlideMovement))]
    public class RotatePlayerMeshFromVelocity : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed, rotationMultiplier;
        [SerializeField] private float minYRot, maxYRot, minVelToRotate, maxVelToRotate;

        private GestureController _gestureController;

        private void Start() 
        {
            _gestureController = GestureController.Instance;     
        }

        private void Update()
        {
            RotateOnVelocity();
        }

        private void RotateOnVelocity()
        {
            Quaternion targetRot = Quaternion.Euler(transform.eulerAngles.x, GetTargetYRot(), transform.eulerAngles.z);

            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, step);
        }

        private float GetTargetYRot()
        {
            Vector3 vel = _gestureController.TouchDelta;
            float newRot = 0;

            if (vel.x > maxVelToRotate)
            {
                newRot = transform.eulerAngles.y + (vel.x * rotationMultiplier);
            }
            else if (vel.x < minVelToRotate)
            {
                newRot = transform.eulerAngles.y + (vel.x * rotationMultiplier);
            }

            newRot = Mathf.Clamp(newRot, minYRot, maxYRot);
            return newRot;
        }
    }
}
