using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GhostSpawner : MonoBehaviour
{
    [Header("Ghost Prefabs (ใส่เรียงจาก LV1 - LV5)")]
    public GameObject[] ghostPrefabs; // ลาก Prefab ผีมาใส่ในนี้ 5 ตัว
    
    [Header("Line Settings")]
    public LineRenderer aimLine; // ลาก AimLine มาใส่ในนี้
    public float maxLineLength = 10f; // ความยาวสูงสุดถ้าไม่ชนอะไรเลย
    public LayerMask groundLayer;    // เลือก Layer ที่เป็นก้นหม้อ (เพื่อไม่ให้เส้นทะลุ)

    [Header("UI Reference")]
    public Image nextGhostUI; 

    [Header("Settings")]
    public float spawnY = 4f;
    public float spawnRangeX = 2.5f;
    public float lineLength = 10f;
    // public float nextDelay = 0.5f;

    private GameObject currentGhostInstance; 
    private int nextGhostLevelIndex; // เก็บเป็น Index (0 คือ LV1, 4 คือ LV5)
    // private bool canPlace = true;
    private bool isAiming = false;

    [Header("Control Settings")]
    private bool isSpawnerActive = true; // ตัวแปรควบคุมเปิด/ปิดระบบปล่อยผี
    // private bool isAiming = false;

    void Start()
    {
        Time.timeScale = 1f; 
        nextGhostLevelIndex = Random.Range(0, 5);
        PrepareGhost();
        if (aimLine) aimLine.enabled = false; // ปิดเส้นเล็งตอนเริ่ม
    }

    void Update()
    {
        if (!isSpawnerActive) return;

        // 1. จังหวะกดนิ้วลง
        if (Input.GetMouseButtonDown(0)) 
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return; 
            }

            isAiming = true;
            if (aimLine) aimLine.enabled = true;
        }

        if (isAiming)
        {
            UpdateSpawnerPosition();
            UpdateAimLine();
        }

        // 2. จังหวะปล่อยนิ้ว
        if (Input.GetMouseButtonUp(0) && isAiming)
        {
            isAiming = false;
            if (aimLine) aimLine.enabled = false;
            DropGhost();
        }
    }
    
    void UpdateSpawnerPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        // ใส่ระยะห่างจากกล้องถึงวัตถุลงไปด้วย (Camera.Z ห่างจาก Spawner.Z เท่าไหร่)
        mousePos.z = -Camera.main.transform.position.z; 
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        float clampedX = Mathf.Clamp(worldPos.x, -spawnRangeX, spawnRangeX);
        transform.position = new Vector3(clampedX, spawnY, 0);

        if (currentGhostInstance != null)
        {
            // ให้ตัวผี Lock ตำแหน่งเดียวกับ Spawner เป๊ะๆ
            currentGhostInstance.transform.position = transform.position;
        }
    }

    public void SetSpawnerActive(bool active)
    {
        isSpawnerActive = active;
        
        // ถ้าถูกสั่งปิด ให้ล้างสถานะเล็งค้างทันที ป้องกันผีหลุดตอนยกนิ้ว
        if (!active)
        {
            isAiming = false;
            if (aimLine) aimLine.enabled = false;
        }
    }

    // void UpdateAimLine()
    // {
    //     if (aimLine != null)
    //     {
    //         // เมื่อ Use World Space = false:
    //         // จุดที่ 0 คือ (0,0,0) หมายถึงเริ่มที่ตัวมันเอง (Spawner)
    //         aimLine.SetPosition(0, Vector3.zero); 
            
    //         // จุดที่ 1 คือ (0, -ความยาว, 0) หมายถึงลากลงไปข้างล่างตรงๆ
    //         aimLine.SetPosition(1, new Vector3(0, -lineLength, 0));
    //     }
    // }
    // void UpdateAimLine()
    // {
    //     if (aimLine != null)
    //     {
    //         // กำหนดความหนาของเส้น (0.05f คือเลขที่คุณลองปรับดูว่าชอบไหม)
    //         aimLine.startWidth = 0.05f; 
    //         aimLine.endWidth = 0.05f;

    //         aimLine.SetPosition(0, Vector3.zero); 
    //         aimLine.SetPosition(1, new Vector3(0, -lineLength, 0));
    //     }
    // }

    void UpdateAimLine()
    {
        if (aimLine == null) 
        {
            return;
        }


        if (aimLine != null) 
        {
            aimLine.SetPosition(0, Vector3.zero);

            aimLine.startWidth = 0.15f;
            aimLine.endWidth = 0.05f;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, maxLineLength, groundLayer);

            if (hit.collider != null)
            {
                float localHitY = hit.point.y - transform.position.y;
                
                aimLine.SetPosition(1, new Vector3(0, localHitY, 0));
            }
            else
            {
                aimLine.SetPosition(1, new Vector3(0, -maxLineLength, 0));
            }
        }
    }

    void PrepareGhost()
    {
        GameObject prefabToSpawn = ghostPrefabs[nextGhostLevelIndex];
        currentGhostInstance = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        
        // Rigidbody2D rb = currentGhostInstance.GetComponent<Rigidbody2D>();
        // if(rb != null) rb.simulated = false;

        Rigidbody2D rb = currentGhostInstance.GetComponent<Rigidbody2D>();
        if(rb != null) 
        {
            rb.simulated = false; // ปิดการจำลองฟิสิกส์ (เส้นแดงจะไม่นับตัวนี้)
        }

        nextGhostLevelIndex = Random.Range(0, 5);
        UpdateNextUI();
    }

    void DropGhost()
    {
        if (currentGhostInstance != null)
        {
            // Rigidbody2D rb = currentGhostInstance.GetComponent<Rigidbody2D>();
            // if(rb != null) rb.simulated = true;
            Rigidbody2D rb = currentGhostInstance.GetComponent<Rigidbody2D>();
            if(rb != null) 
            {
                rb.simulated = true; // เปิดการจำลองฟิสิกส์
            }
            currentGhostInstance = null;
            Invoke("PrepareGhost", 0.8f); // ดีเลย์นิดนึงก่อนตัวถัดไปจะโผล่มาบนมือ
        }
    }

    void UpdateNextUI()
    {
        if (nextGhostUI != null)
        {
            SpriteRenderer sr = ghostPrefabs[nextGhostLevelIndex].GetComponent<SpriteRenderer>();
            nextGhostUI.sprite = sr.sprite;
            nextGhostUI.color = sr.color;
        }
    }
}