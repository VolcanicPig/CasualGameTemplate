using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    public class PlayerRotateMovement : PlayerMovement
    {
        [SerializeField] private float rotateSpeed; 

        private GestureController _gestures;
        
        private void Start() 
        {
            _gestures = GestureController.Instance; 
        }
        
        private void Update()
        {
            HandleRotation();
        }

        private void HandleRotation()
        {
            Vector2 touchDelta = _gestures.TouchDelta;
            float step = rotateSpeed * Time.deltaTime; 

            transform.Rotate(Vector3.up * touchDelta.x * step); 
        }
    }
}
