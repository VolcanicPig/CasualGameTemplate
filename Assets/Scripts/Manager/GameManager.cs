using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile; 

namespace Game
{
    public class GameManager : MobileGameManagerTemplate<GameManager>
    {
        private void Update() 
        {
#if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.R))
            {
                EndGame(true);  
            }
#endif    
        }
    }
}
