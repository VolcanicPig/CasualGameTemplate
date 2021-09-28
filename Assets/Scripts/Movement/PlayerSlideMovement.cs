using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerSlideMovement : PlayerMovement
    {
        [Header("Components")]
        [SerializeField] private float sideMult, sideSpeed;
        
        private GestureController _gestures; 

        private void Start() 
        {
            _gestures = GestureController.Instance;     
        }

        private void Update()
        {
            SidewaysMovement();
        }

        private void SidewaysMovement()
        {
            if (!CanMove) return;
            Vector3 cachedPosition = transform.position;
            Vector2 touchDelta = _gestures.TouchDelta;

            cachedPosition.x += touchDelta.x * sideMult * Time.deltaTime;
            cachedPosition.x = Mathf.Clamp(cachedPosition.x, -5, 5);

            float step = sideSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, cachedPosition, step);
        }
    }
}
