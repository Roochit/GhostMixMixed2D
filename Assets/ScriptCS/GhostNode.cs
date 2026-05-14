using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNode : MonoBehaviour
{
    public int ghostLevel; // ระดับของผี (เช่น LV1 คือบอลขาว, LV2 คือบอลแดง)
    public GameObject nextLevelPrefab; // ลาก Prefab ตัวต่อไปมาใส่ใน Inspector
    public int scoreValue;
    private bool isMerged = false; // ป้องกันการชนครั้งเดียวแล้วเกิดซ้ำสองรอบ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
                    
                    // สร้างตัวใหม่
                    GameObject nextGhost = Instantiate(nextLevelPrefab, spawnPos, Quaternion.identity);
                    
                    // --- เพิ่มระบบคะแนนตรงนี้ ---
                    // ดึงค่าคะแนนจากผีตัวใหม่ที่เพิ่งสร้างขึ้นมา
                    int points = nextGhost.GetComponent<GhostNode>().scoreValue;
                    ScoreManagerCS.instance.AddScore(points);
                    // -----------------------

                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }
            }
        }
    }

}
