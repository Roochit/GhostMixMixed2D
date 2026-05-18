using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameOverManagerCS : MonoBehaviour
{
    public static GameOverManagerCS instance; // เพื่อให้ GhostNode เรียกใช้ได้ง่าย

    public float timeToFail = 10f;
    public TextMeshProUGUI countdownText;
    [Header("Lose Settings")]
    public GameObject gameOverPanel;
    public string loseScene;

    [Header("Win Settings")]
    public GameObject winPanel;


    private float timer;
    private HashSet<GameObject> failingGhosts = new HashSet<GameObject>(); // เก็บผีที่ทำผิดกฎ

    void Awake()
    {
        instance = this; // บังคับให้ instance ชี้มาที่ตัวใหม่ที่เพิ่งโหลดขึ้นมาเสมอ
    }

    void Start()
    {
        Time.timeScale = 1f; 
        timer = timeToFail;
        if (countdownText) countdownText.gameObject.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }



    // ผีจะส่งชื่อมาเข้าชื่อเมื่อล้นเส้น
    // public void ReportGhostOverLine(GameObject ghost)
    // {
    //     if (!failingGhosts.Contains(ghost))
    //     {
    //         failingGhosts.Add(ghost);
    //     }
    // }
    public void ReportGhostOverLine(GameObject ghost)
    {
        Debug.Log("Ghost is over line: " + ghost.name); // ถ้าบรรทัดนี้ไม่ขึ้นใน Console แสดงว่าเป็นที่ตัวผีไม่ยอมส่งรายงานมา
        if (!failingGhosts.Contains(ghost))
        {
            failingGhosts.Add(ghost);
        }
    }

    // ผีจะแจ้งเมื่อตัวเองกลับลงไปใต้เส้นแล้ว
    public void ReportGhostSafe(GameObject ghost)
    {
        if (failingGhosts.Contains(ghost))
        {
            failingGhosts.Remove(ghost);
        }
    }

    void Update()
    {
        // ล้างขยะ (ผีที่ถูกทำลายไปแล้ว)
        failingGhosts.RemoveWhere(ghost => ghost == null);

        if (failingGhosts.Count > 0)
        {
            timer -= Time.deltaTime;
            if (countdownText)
            {
                countdownText.gameObject.SetActive(true);
                countdownText.text = "CAUTION! " + Mathf.Ceil(timer).ToString();
            }
            if (timer <= 0) GameOver();
        }
        else
        {
            timer = timeToFail;
            if (countdownText) countdownText.gameObject.SetActive(false);
        }
    }

    void GameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void WinGame()
    {
        // หยุดเกมและแสดงหน้าจอชนะ
        if (winPanel) winPanel.SetActive(true);
        
        // อาจจะรอสัก 1-2 วินาทีให้คนดูผีตัวสุดท้ายก่อนค่อยหยุดเวลาก็ได้
        Time.timeScale = 0f; 
        Debug.Log("Congratulations! Game Clear.");
        // Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        if(failingGhosts != null) failingGhosts.Clear();
        SceneManager.LoadScene(loseScene);
    }
}