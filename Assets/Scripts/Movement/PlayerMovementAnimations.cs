using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerMovementAnimations : MonoBehaviour
    {
        private const string RunKey = "Running";

        [Header("References")]
        [SerializeField] private Animator anim;

        private bool _running = false;

        private void Update()
        {
            anim.SetBool(RunKey, _running);
        }

        public void SetRunning(bool isRunning) => _running = isRunning;
    }
}
