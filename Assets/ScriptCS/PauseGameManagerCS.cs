using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseGameManagerCS : MonoBehaviour
{
    public GameObject PauseUI;
    public AudioSource gameplayBGM;
    public GhostSpawner ghostSpawner;

    // public AudioSource lobbyBGM; 
    // public GameObject settingsPanel;
    
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
        if  (gameplayBGM != null) 
        {
            gameplayBGM.Pause();
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
        if (gameplayBGM != null) 
        {   
            gameplayBGM.UnPause();
        }

        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
