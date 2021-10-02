using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig;
using VolcanicPig.Mobile;
using Random = UnityEngine.Random;


namespace Game
{
    public class LevelManager : SingletonBehaviour<LevelManager>
    {
        public Level[] levelPrefabs; 
        private Level _currentLevelObject;
        private Level lastlevelIndex;  
        public Level GetCurrentLevelObj => _currentLevelObject; 

        [Header("Debug")]
        [SerializeField] private bool forceLevel; 
        [SerializeField] private int forceLevelNum;

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
            if (state == GameState.Start)
            {
                SpawnInLevel();
            }
        }

        private void SpawnInLevel()
        {
            if(_currentLevelObject != null)
            {
                Destroy(_currentLevelObject.gameObject); 
            }

            int level = GameManager.Instance.Level;

#if UNITY_EDITOR
            if(forceLevel)
            {
                level = forceLevelNum; 
            }
#endif
            List<Level> availableLevels = new List<Level>(levelPrefabs); 
            if(lastlevelIndex != null) availableLevels.Remove(lastlevelIndex); 


            if(level >= levelPrefabs.Length)
            {
                Level randLevel = availableLevels[Random.Range(0, availableLevels.Count)];
                _currentLevelObject = Instantiate(randLevel, Vector3.zero, Quaternion.identity);
                lastlevelIndex = randLevel; 
            }
            else
            {
                Level l = levelPrefabs[level]; 
                _currentLevelObject = Instantiate(l, Vector3.zero, Quaternion.identity); 
                lastlevelIndex = l; 
            }
        }
    }
}