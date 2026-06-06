using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomTextCS : MonoBehaviour
{
    [Header("UI References")]
    public Image targetImageUI;
    // public TextMeshProUGUI targetText;

    [Header("คลังรูปภาพที่ต้องการเอามาสุ่ม (Sprites)")]
    public List<Sprite> ghostSprites = new List<Sprite>();

    void OnEnable()
    {
        DisplayRandomContent();
    }

    public void DisplayRandomContent()
    {
        // 1. ระบบสุ่มรูปภาพ
        if (ghostSprites != null && ghostSprites.Count > 0 && targetImageUI != null)
        {
            // สุ่มเลข Index ของรูปภาพ
            int randomImageIndex = Random.Range(0, ghostSprites.Count);
            
            
            // เปลี่ยนภาพสไปรท์บนหน้าจอตามตัวที่สุ่มได้
            targetImageUI.sprite = ghostSprites[randomImageIndex];
        }
        else
        {
            Debug.LogWarning("เพื่อนรัก! ลืมลาก Component Image หรือลืมใส่รูปในลิสต์หรือเปล่า");
        }

        // // 2. ระบบสุ่มข้อความคำคม (ทำงานควบคู่กันไปเลย)
        // if (textList != null && textList.Count > 0 && targetText != null)
        // {
        //     int randomTextIndex = Random.Range(0, textList.Count);
        //     targetText.text = textList[randomTextIndex];
        // }
    }

    public void ForceRandom()
    {
        DisplayRandomContent();
    }

    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
