using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostNode : MonoBehaviour
{
    public int ghostLevel;
    public int scoreValue;
    public GameObject nextLevelPrefab;
    
    private bool isMerged = false;
    private bool hasLanded = false; // เช็คว่าชนกับผีตัวอื่นหรือพื้นหรือยัง
    private Rigidbody2D rb;

    void Start()
    {
        Time.timeScale = 1f; 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // เงื่อนไข Game Over:
        // 1. ต้องปล่อยลงมาแล้ว (simulated)
        // 2. ต้องเคยชนกับอะไรบางอย่างด้านล่างแล้ว (hasLanded)
        // 3. ตำแหน่ง Y สูงกว่าเส้นสีแดง
        if (rb != null && rb.simulated && hasLanded)
        {
            float lineY = GameOverManagerCS.instance.transform.position.y;
            if (transform.position.y > lineY)
            {
                GameOverManagerCS.instance.ReportGhostOverLine(gameObject);
            }
            else
            {
                GameOverManagerCS.instance.ReportGhostSafe(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GhostNode otherGhost = collision.gameObject.GetComponent<GhostNode>();

        if (otherGhost != null && !isMerged && !otherGhost.isMerged)
        {
            if (otherGhost.ghostLevel == this.ghostLevel)
            {
                // ถ้าตัวที่เรากำลังจะผสม มีตัวถัดไป (ยังไม่ใช่ตัวสุดท้าย)
                if (nextLevelPrefab != null)
                {
                    isMerged = true;
                    otherGhost.isMerged = true;

                    Vector3 spawnPos = (transform.position + collision.transform.position) / 2f;
                    GameObject nextGhost = Instantiate(nextLevelPrefab, spawnPos, Quaternion.identity);
                    
                    // ตรวจสอบว่า "ตัวที่เพิ่งสร้าง" เป็นตัวสุดท้ายหรือไม่
                    GhostNode nextNode = nextGhost.GetComponent<GhostNode>();
                    if (nextNode.nextLevelPrefab == null)
                    {
                        // ถ้าตัวใหม่ไม่มีตัวไปต่อแล้ว = จบเกม (Win)
                        Debug.Log("You reached the Final Ghost!");
                        GameOverManagerCS.instance.WinGame(); 
                    }

                    // ระบบคะแนนเดิม
                    int points = nextNode.scoreValue;
                    if(ScoreManagerCS.instance != null) ScoreManagerCS.instance.AddScore(points);

                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}