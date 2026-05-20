using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseGameManagerCS : MonoBehaviour
{
    public GameObject PauseUI;
    public GhostSpawner ghostSpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickPause()
    {
        if (ghostSpawner != null) 
        {
            ghostSpawner.SetSpawnerActive(false); 
        }
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClickContinue()
    {
        if (ghostSpawner != null) 
        {
            ghostSpawner.SetSpawnerActive(true); 
        }

        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
