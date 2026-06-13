using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingGameManager : MonoBehaviour
{
    public AudioSource lobbyBGM; 
    public GameObject settingsPanel;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void OpenSettingsLobby()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true); // เปิดหน้าต่างตั้งค่า
        }
    }
    public void CloseSettingsLobby()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false); // ปิดหน้าต่างตั้งค่ากลับหน้าเดิม
        }

        // 🎵 บังคับสั่งให้เพลงเล่นต่อชัวร์ๆ ทันทีที่ปิดหน้าต่างกลับมา
        if (lobbyBGM != null)
        {
            if (!lobbyBGM.isPlaying)
            {
                lobbyBGM.UnPause(); // ถ้ามันเคยเผลอโดนสั่ง Pause ไว้ ให้ UnPause ซะ
                lobbyBGM.Play();    // ชวนให้มั่นใจว่าเล่นแน่นอน
            }
            
            // lobbyBGM.volume = 1f; // ดึงวอลลุ่มกลับมาดังเต็มที่ถ้าเคยหรี่ไว้
        }
    }
}
