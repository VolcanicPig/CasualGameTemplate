using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VolcanicPig.Mobile.Ui; 
using TMPro;
using UnityEngine.UI;
using VolcanicPig;

namespace Game
{
    public class StartMenu : Menu
    {
        [SerializeField]  private TextMeshProUGUI titleText;

        [Header("Settings")] 
        [SerializeField] private CanvasGroup settingsButtonsCanvas; 
        [SerializeField] private Sprite tickSprite, crossSprite;
        [SerializeField] private Image hapticsTick, soundTick;

        private Settings _settings; 
        
        public void StartGamePressed()
        {
            GameManager.Instance.StartGame();
        }

        public override void OnMenuOpened()
        {
            base.OnMenuOpened();
            
            _settings = GameManager.Instance.Settings;
            UpdateSettingsSprites();
            
            if(titleText) titleText.text = $"Level {GameManager.Instance.Level}"; 
        }

        public void SettingsButtonPressed()
        {
            bool enable = !settingsButtonsCanvas.interactable;
            settingsButtonsCanvas.interactable = enable;
            settingsButtonsCanvas.blocksRaycasts = enable;
            settingsButtonsCanvas.DOFade(enable ? 1 : 0, .5f); 
        }
        
        public void ToggleHapticsPressed()
        {
            _settings.ToggleHapticsEnabled();
            hapticsTick.sprite = _settings.HapticsEnabled ? tickSprite : crossSprite; 
            hapticsTick.color = _settings.HapticsEnabled ? Color.green : Color.red; 
        }

        public void ToggleSoundPressed()
        {
            _settings.ToggleSoundEnabled();
            soundTick.sprite = _settings.SoundEnabled ? tickSprite : crossSprite;
            soundTick.color = _settings.SoundEnabled ? Color.green : Color.red; 
        }

        private void UpdateSettingsSprites()
        {
            hapticsTick.sprite = _settings.HapticsEnabled ? tickSprite : crossSprite; 
            hapticsTick.color = _settings.HapticsEnabled ? Color.green : Color.red; 
            soundTick.sprite = _settings.SoundEnabled ? tickSprite : crossSprite;
            soundTick.color = _settings.SoundEnabled ? Color.green : Color.red; 
        }
    }
}
