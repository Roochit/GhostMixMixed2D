using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneBottonManagerCS : MonoBehaviour
{
    public string NextScene ;
    
    public float delayTime = 0.2f; 
    private AudioSource NextSceneAudio;
    
    void Start()
    {
        Time.timeScale = 1f; 
        NextSceneAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void OnClickChangeScene()
    {
        StartCoroutine(PlayAnimationAndLoad());
    }

    IEnumerator PlayAnimationAndLoad()
    {

        PlayNextSceneSound();
        
        transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);
        
        
        yield return new WaitForSeconds(delayTime / 2);
        
        
        transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        
        yield return new WaitForSeconds(delayTime / 2);

   
        SceneManager.LoadScene(NextScene);
    }

    public void PlayNextSceneSound()
    {
        if (NextSceneAudio != null)
        {
            NextSceneAudio.PlayOneShot(NextSceneAudio.clip);
        }
        else
        {
            Debug.LogWarning("ลืมใส่ Component AudioSource หรือเปล่าเพื่อน !");
            Debug.LogWarning("หรือตรงนี้ๆไม่ต้องหารใช้ Component AudioSource ?");
        }
    }
}
