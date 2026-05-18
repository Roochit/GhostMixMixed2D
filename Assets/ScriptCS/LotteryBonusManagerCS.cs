using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LotteryBonusManagerCS : MonoBehaviour
{

    [Header("Lucky Number Settings")]
    public TextMeshProUGUI luckyNumberText; 
    public float rollDuration = 2.0f; // ระยะเวลาที่เลขจะวิ่ง (วินาที)
    public float interval = 0.05f;

    [Header("Back to Game Settings")]
    public GameObject BackToGameButton;
    
    void Start()
    {
        StartCoroutine(RollNumberRoutine());
        // BackToGameButton.SetActive(true);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    // public void GenerateLuckyNumber()
    // {
    //     // สุ่มเลข 0 ถึง 99
    //     int randomNumber = Random.Range(0, 100);

    //     // แสดงผลโดยใช้ "D2" เพื่อให้เลขหลักเดียวมี 0 นำหน้า (เช่น 05, 09)
    //     if (luckyNumberText != null)
    //     {
    //         luckyNumberText.text = randomNumber.ToString("D2");
    //     }
    // }

    IEnumerator RollNumberRoutine()
    {
        float elapsed = 0f;
        int finalNumber = Random.Range(0, 100); // สุ่มเลขจริงที่จะออกไว้ก่อน

        // ช่วงที่เลขกำลังวิ่ง
        while (elapsed < rollDuration)
        {
            // สุ่มเลขหลอกๆ แสดงบน UI ไปเรื่อยๆ
            int tempNumber = Random.Range(0, 100);
            luckyNumberText.text = tempNumber.ToString("D2");

            // รอตามเวลา interval ที่กำหนด
            elapsed += interval;
            yield return new WaitForSeconds(interval);
        }

        // เมื่อครบเวลา ให้หยุดที่เลขจริงที่สุ่มไว้ตอนแรก
        luckyNumberText.text = finalNumber.ToString("D2");
        
        // เพิ่มลูกเล่น: ขยายขนาดเล็กน้อยเมื่อหยุด
        luckyNumberText.transform.localScale = Vector3.one * 1.2f;
        // คืนค่าขนาดปกติ (ใช้ Coroutine ย่อยหรือ Simple Tween ก็ได้)
        // LeanTween.scale(luckyNumberText.gameObject, Vector3.one, 0.2f); 
        float finishElapsed = 0;
        float finishDuration = 0.2f;
        Vector3 startScale = luckyNumberText.transform.localScale;
        Vector3 endScale = Vector3.one;

        while (finishElapsed < finishDuration)
        {
            finishElapsed += Time.deltaTime;
            luckyNumberText.transform.localScale = Vector3.Lerp(startScale, endScale, finishElapsed / finishDuration);
            yield return null;
        }

        BackToGameButton.SetActive(true);
    }
}
