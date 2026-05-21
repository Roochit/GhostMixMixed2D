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
    private bool hasLanded = false; 
    private Rigidbody2D rb;

    void Start()
    {
        Time.timeScale = 1f; 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

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
        if (collision.gameObject.CompareTag("Ghost") || 
            collision.gameObject.name.Contains("Square"))
        {
            hasLanded = true;
            // Debug.Log(gameObject.name + " แตะหม้อหรือแตะเพื่อนแล้ว -> พร้อมนับถอยหลัง");
        }

        // --- ส่วนการผสมผี (Merge) คงเดิม ---
        GhostNode otherGhost = collision.gameObject.GetComponent<GhostNode>();
        if (otherGhost != null && !isMerged && !otherGhost.isMerged)
        {
            if (otherGhost.ghostLevel == this.ghostLevel)
            {
                if (nextLevelPrefab != null)
                {
                    isMerged = true;
                    otherGhost.isMerged = true;
                    Vector3 spawnPos = (transform.position + collision.transform.position) / 2f;
                    GameObject nextGhost = Instantiate(nextLevelPrefab, spawnPos, Quaternion.identity);
                    
                    GhostNode nextNode = nextGhost.GetComponent<GhostNode>();
                    if (nextNode.nextLevelPrefab == null)
                    {
                        GameOverManagerCS.instance.WinGame(); 
                    }

                    int points = nextNode.scoreValue;
                    if(ScoreManagerCS.instance != null) 
                    {
                        ScoreManagerCS.instance.AddScore(points);
                    }

                    if (AudioManagerCS.instance != null)
                    {
                        AudioManagerCS.instance.PlayFusionSound();
                    }


                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}