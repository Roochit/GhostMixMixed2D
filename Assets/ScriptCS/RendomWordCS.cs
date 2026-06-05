using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RendomWordCS : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI targetText;


    [Header("ประโยคคำที่ต้องการสุ่ม")]
    [TextArea(2, 5)] //ขนาดช่องพิมพ์ข้อความ
    public List<string> textList = new List<string>();

    [Header("การตั้งค่าเส้นขอบ (Stroke/Outline)")]
    public bool useOutline = true;              // เปิด/ปิด การใช้เส้นขอบ
    public Color outlineColor = Color.black;      // สีของเส้นขอบ (ปรับใน Inspector ได้)
    [Range(0f, 1f)]
    public float outlineThickness = 0.3f;

    void OnEnable()
    {
        DisplayRandomText();
    }

    public void DisplayRandomText()
    {
        
        if (textList == null || textList.Count == 0)
        {
            Debug.LogWarning("เพื่อน! ลืมใส่ประโยคในลิสต์หรือเปล่า");
            return;
        }

        if (targetText == null)
        {
            targetText = GetComponent<TextMeshProUGUI>(); 
        }

        if (targetText != null)
        {

            int randomIndex = Random.Range(0, textList.Count);

            targetText.text = textList[randomIndex];
        }

        if (targetText != null)
        {
            // 1. สุ่มข้อความธรรมดามาใส่
            int randomIndex = Random.Range(0, textList.Count);
            targetText.text = textList[randomIndex];

            // 2. สั่งเปิดระบบ Stroke และพ่นสีจากโค้ด C# เข้าไปที่ Material ตรงๆ
            if (useOutline)
            {
                // เปิดฟีเจอร์ขอบให้กับ Material ของฟอนต์ตัวนั้นในเฟรมนี้
                targetText.fontMaterial.EnableKeyword("OUTLINE_ON");
                
                // สั่งเซตสีและค่าความหนาขอบตาที่เราตั้งไว้
                targetText.outlineColor = outlineColor;
                targetText.outlineWidth = outlineThickness;
            }
            else
            {
                // ถ้าไม่ใช้ ให้สั่งปิดขอบ
                targetText.fontMaterial.DisableKeyword("OUTLINE_ON");
                targetText.outlineWidth = 0f;
            }

            // สั่งให้ TMP อัปเดตการแสดงผลหน้าจอทันที
            targetText.UpdateMeshPadding();
        }
    }

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
