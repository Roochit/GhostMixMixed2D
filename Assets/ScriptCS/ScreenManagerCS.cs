using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManagerCS : MonoBehaviour
{
    void Awake() 
    {
        #if UNITY_STANDALONE
            int WidthScale = 600 ;
            int HeightScale = 800 ;

            Screen.SetResolution(WidthScale, HeightScale , FullScreenMode.Windowed);

            Screen.fullScreen = false;
        #endif
    }
}
