using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;
using UnityEngine.EventSystems;

namespace Game
{
    public class VpButton : Button
    {
        [Header("SFX")] 
        [SerializeField] private string pressSuccessSound = "buttonPress_success"; 
        [SerializeField] private string pressFailSound = "buttonPress_fail"; 
        
        [Header("Haptics")]
        [SerializeField] private HapticTypes pressHapticsSuccess = HapticTypes.Selection;
        [SerializeField] private HapticTypes pressHapticsFail = HapticTypes.Warning;

        [Header("Animation")] 
        [SerializeField] private bool doPressAnimation = true;
        [SerializeField] private Vector3 animationPunch = new Vector3(-0.2f, -0.2f, -0.2f); 
        
        private RectTransform _rect;
        private Vector3 _startButtonScale; 

        protected override void Awake()
        {
            base.Awake();
            _rect = GetComponent<RectTransform>();
            _startButtonScale = _rect.localScale; 
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            PlayButtonEffects();
            base.OnSubmit(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            PlayButtonEffects();
            base.OnPointerClick(eventData);
        }

        private void PlayButtonEffects()
        {
            if (!IsInteractable())
            {
                if(pressHapticsFail != HapticTypes.None)
                {
                    MMVibrationManager.Haptic(pressHapticsFail);
                }

                if (pressFailSound != string.Empty)
                {
                    AudioManager.Instance.PlayClip(pressFailSound); 
                }
                
                if (doPressAnimation)
                {
                    _rect.localScale = _startButtonScale; 
                    _rect.DOPunchScale(animationPunch, .2f, 1, 0).OnComplete(CheckButtonScale); 
                }
            }
            else
            {
                if(pressHapticsSuccess != HapticTypes.None)
                {
                    MMVibrationManager.Haptic(pressHapticsSuccess);
                }  
                
                if (pressSuccessSound != string.Empty)
                {
                    AudioManager.Instance.PlayClip(pressSuccessSound); 
                }

                if (doPressAnimation)
                {
                    _rect.localScale = _startButtonScale; 
                    _rect.DOPunchScale(animationPunch, .2f, 1, 0).OnComplete(CheckButtonScale); 
                }
            }
        }

        private void CheckButtonScale()
        {
            if (_rect.localScale != _startButtonScale)
            {
                _rect.localScale = _startButtonScale; 
            }
        }
    }
}
