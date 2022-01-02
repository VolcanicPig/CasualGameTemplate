using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 
using VolcanicPig;
using VolcanicPig.Mobile;
using VolcanicPig.Utilities;

namespace Game
{
    public class CameraController : SingletonBehaviourSerialized<CameraController>
    {
        public Dictionary<string, CinemachineVirtualCameraBase> cameraMap;

        [SerializeField] private FollowTargetsPosition cameraTarget; 
        
        private CinemachineVirtualCameraBase _currCamera;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged; 
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged; 
        }

        private void Start()
        {
            SwapCamera("Start");
        }

        public void SetFollowTarget(Transform target)
        {
            cameraTarget.SetTarget(target); 
        }

        private void OnGameStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Start:
                    SwapCamera("Start");
                    break;
                case GameState.InGame:
                    SwapCamera("InGame");
                    break;
                case GameState.End:
                    SwapCamera("End");
                    break;
            }
        }

        public void SwapCamera(string key)
        {
            if (!GetCameraByKey(key, out CinemachineVirtualCameraBase newCamera)) return;
            
            if (_currCamera != null)
            {
                _currCamera.m_Priority = -1; 
            }

            newCamera.m_Priority = 1;
            _currCamera = newCamera;
        }

        private bool GetCameraByKey(string key, out CinemachineVirtualCameraBase cameraBase)
        {
            foreach (KeyValuePair<string, CinemachineVirtualCameraBase> pair in cameraMap)
            {
                if (pair.Key == key)
                {
                    cameraBase = pair.Value;
                    return true; 
                } 
            }

            cameraBase = null;
            return false; 
        }
    }
}
