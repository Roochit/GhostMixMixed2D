using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // อย่าลืมใช้ TextMeshPro เพื่อความสวยงามแบบในรูปของคุณ

public class ScoreManagerCS : MonoBehaviour
{
    public static ScoreManagerCS instance; // สร้าง Singleton เพื่อให้สคริปต์อื่นเรียกใช้ง่ายๆ

    public TextMeshProUGUI scoreText; // ลากตัวหนังสือ Score ใน UI มาใส่
    private int totalScore = 0;

    public void Start()
    {
        Time.timeScale = 1f; 
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "คะแนน : " + totalScore.ToString();
    }
}