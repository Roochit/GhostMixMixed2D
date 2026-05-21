using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerCS : MonoBehaviour
{
    public static AudioManagerCS instance; 
    private AudioSource audioSource;
   
   void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.Stop();
    }

    
    void Update()
    {
        
    }
    public void PlayFusionSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        else
        {
            Debug.LogWarning("ลืมใส่ Component AudioSource หรือเปล่าเพื่อน!");
        }
    }
}
