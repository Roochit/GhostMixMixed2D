using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBottonManagerCS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void ExitGame()
    {
        
        Application.Quit();

        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
