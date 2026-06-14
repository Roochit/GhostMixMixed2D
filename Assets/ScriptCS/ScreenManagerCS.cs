using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManagerCS : MonoBehaviour
{
    void Awake() 
    {
        int WidthScale = 600 ;
        int HeightScale = 800 ;

        Screen.SetResolution(WidthScale, HeightScale , FullScreenMode.Windowed);
    }
}
