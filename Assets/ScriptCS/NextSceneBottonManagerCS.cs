using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneBottonManagerCS : MonoBehaviour
{
    public string NextScene ;
    // public string sceneName; // พิมพ์ชื่อ Scene ที่จะไปใน Inspector
    public float delayTime = 0.2f; // ระยะเวลาขยาย-ย่อ
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; 
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
        // 1. เล่นอนิเมชั่นขยาย (ใช้โค้ดคุม Scale)
        transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);
        
        // รอแป๊บหนึ่ง
        yield return new WaitForSeconds(delayTime / 2);
        
        // 2. เล่นอนิเมชั่นย่อกลับ
        transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        
        yield return new WaitForSeconds(delayTime / 2);

        // 3. เปลี่ยนหน้า
        // SceneManager.LoadScene(sceneName);
        SceneManager.LoadScene(NextScene);
    }

    // void NextSceneFunction()
    // {
    //     SceneManager.LoadScene(NextScene);
    // }
}
