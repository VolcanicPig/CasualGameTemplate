using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Collectable;
using VolcanicPig.Mobile.Ui;
using MoreMountains.NiceVibrations;

namespace Game
{
    public class CoinCollectable : Collectable
    {
        [SerializeField] private int coinValue; 
        
        public override void Collect()
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            FeedbackEffects.Instance.DoCoinFeedback(coinValue, transform.position, fromWorldPosition: true); 
            GameManager.Instance.Currency += coinValue; 
            base.Collect();
        }
    }
}
