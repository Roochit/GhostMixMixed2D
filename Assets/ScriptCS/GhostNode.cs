using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNode : MonoBehaviour
{
    public int ghostLevel; // ระดับของผี (เช่น LV1 คือบอลขาว, LV2 คือบอลแดง)
    public GameObject nextLevelPrefab; // ลาก Prefab ตัวต่อไปมาใส่ใน Inspector
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
        // ตรวจสอบว่าชนกับ Object ที่มีสคริปต์ GhostNode เหมือนกัน
        GhostNode otherGhost = collision.gameObject.GetComponent<GhostNode>();

        if (otherGhost != null && !isMerged && !otherGhost.isMerged)
        {
            // เงื่อนไข: ต้องเป็นผีระดับเดียวกัน
            if (otherGhost.ghostLevel == this.ghostLevel)
            {
                // ตรวจสอบ Next Level Prefab (ถ้าเป็นตัวสูงสุดแล้วจะไม่เกิดอะไรขึ้น)
                if (nextLevelPrefab != null)
                {
                    isMerged = true;
                    otherGhost.isMerged = true;

                    // คำนวณจุดกึ่งกลางระหว่าง 2 ตัว เพื่อสร้างตัวใหม่
                    Vector3 spawnPos = (transform.position + collision.transform.position) / 2f;

                    // สร้างผีระดับถัดไป (เช่น บอลแดง)
                    Instantiate(nextLevelPrefab, spawnPos, Quaternion.identity);

                    // ทำลายผีตัวเก่าทั้ง 2 ตัว
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
