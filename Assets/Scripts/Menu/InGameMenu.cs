using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Ui; 
using TMPro;

namespace Game
{
    public class InGameMenu : Menu
    {
        [SerializeField] private TMP_Text currencyText; 

        private void OnEnable()
        {
            GameManager.OnCurrencyChanged += OnCurrencyUpdated;  
        }

        private void OnDisable()
        {
            GameManager.OnCurrencyChanged -= OnCurrencyUpdated;
        }

        private void OnCurrencyUpdated(int oldValue, int newValue)
        {
            currencyText.text = newValue.ToString();
        }
    }
}
