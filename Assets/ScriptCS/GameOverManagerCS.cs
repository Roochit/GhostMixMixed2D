using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // สำหรับแสดงตัวเลขแจ้งเตือน
using UnityEngine.SceneManagement;
// using System.Collections.Generic;
// using System.Collections;

public class GameOverManagerCS : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToFail = 10f; // เวลานับถอยหลัง
    public TextMeshProUGUI countdownText; // UI แสดงตัวเลข 10, 9, 8...
    public GameObject gameOverPanel; // หน้าต่างตอนแพ้

    

    private float timer;
    private bool isCountingDown = false;
    private List<GameObject> ghostsOverLine = new List<GameObject>();

    void Start()
    {
        timer = timeToFail;
        if (countdownText) countdownText.gameObject.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // 1. ลบผีที่เป็น Null (ถูกทำลายจากการรวมร่าง) ออกจาก List
        ghostsOverLine.RemoveAll(ghost => ghost == null);

        // 2. [เพิ่ม] ลบผีที่ตำแหน่ง Y ต่ำกว่าเส้น (ร่วงลงหม้อไปแล้ว) 
        // เพื่อป้องกันกรณี TriggerExit ไม่ทำงาน
        float lineY = transform.position.y;
        ghostsOverLine.RemoveAll(ghost => ghost.transform.position.y < lineY);

        if (ghostsOverLine.Count > 0)
        {
            isCountingDown = true;
            timer -= Time.deltaTime;

            if (countdownText)
            {
                countdownText.gameObject.SetActive(true);
                countdownText.text = "CAUTION! " + Mathf.Ceil(timer).ToString();
                countdownText.color = Color.red;
            }

            if (timer <= 0)
            {
                GameOver();
            }
        }
        else
        {
            isCountingDown = false;
            timer = timeToFail;
            if (countdownText) countdownText.gameObject.SetActive(false);
        }
    }

    // ตรวจจับเมื่อผีแตะเส้น
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            
            // เงื่อนไข: ต้องเป็นผีที่ปล่อยแล้ว (simulated == true) 
            // และยังไม่อยู่ใน List ของเรา
            if (rb != null && rb.simulated && !ghostsOverLine.Contains(collision.gameObject))
            {
                ghostsOverLine.Add(collision.gameObject);
            }
        }
    }

    // ตรวจจับเมื่อผีออกจากเส้น (เช่น ตกลงไปข้างล่าง หรือรวมร่างหายไป)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost"))
        {
            // เมื่อผีตกลงไปข้างล่าง หรือถูกดีดออกไป ให้ลบออกจาก List
            if (ghostsOverLine.Contains(collision.gameObject))
            {
                ghostsOverLine.Remove(collision.gameObject);
            }
        }
    }

    void GameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Time.timeScale = 0; // หยุดเกม
        Debug.Log("Game Over!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
