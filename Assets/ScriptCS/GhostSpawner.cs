using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GhostSpawner : MonoBehaviour
{
    [Header("Ghost Prefabs (ใส่เรียงจาก LV1 - LV5)")]
    public GameObject[] ghostPrefabs; // ลาก Prefab ผีมาใส่ในนี้ 5 ตัว

    [Header("UI Reference")]
    public Image nextGhostUI; 

    [Header("Settings")]
    public float spawnY = 4f;
    public float spawnRangeX = 2.5f;
    public float nextDelay = 0.5f;

    private GameObject currentGhostInstance; 
    private int nextGhostLevelIndex; // เก็บเป็น Index (0 คือ LV1, 4 คือ LV5)
    private bool canPlace = true;

    void Start()
    {
        // สุ่มตัวแรกที่จะโผล่ในมือ (Index 0 ถึง 4)
        nextGhostLevelIndex = Random.Range(0, 5); 
        SpawnNextInHand();
    }

    void Update()
    {
        // 1. เลื่อนตำแหน่ง Spawner ตามเมาส์
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(mousePos.x, -spawnRangeX, spawnRangeX);
        transform.position = new Vector3(clampedX, spawnY, 0);

        if (currentGhostInstance != null)
        {
            currentGhostInstance.transform.position = transform.position;
        }

        // 2. คลิกปล่อย
        if (Input.GetMouseButtonDown(0) && canPlace && currentGhostInstance != null)
        {
            DropGhost();
        }
    }

    void SpawnNextInHand()
    {
        // สร้างผีในมือจาก Index ที่สุ่มไว้
        GameObject prefabToSpawn = ghostPrefabs[nextGhostLevelIndex];
        currentGhostInstance = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        
        Rigidbody2D rb = currentGhostInstance.GetComponent<Rigidbody2D>();
        if(rb != null) rb.simulated = false;

        // สุ่มตัว "ถัดไป" ใหม่ (0-4)
        nextGhostLevelIndex = Random.Range(0, 5);
        UpdateNextUI();
    }

    void UpdateNextUI()
    {
        if (nextGhostUI != null && ghostPrefabs.Length > nextGhostLevelIndex)
        {
            // ดึง Sprite และสี จาก Prefab ตัวถัดไปมาแสดงบน UI โดยอัตโนมัติ
            SpriteRenderer sr = ghostPrefabs[nextGhostLevelIndex].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                nextGhostUI.sprite = sr.sprite;
                nextGhostUI.color = sr.color; // ถ้าใช้บอลสีเดิมแต่เปลี่ยนสีใน Unity ตัวนี้จะช่วยให้ UI เปลี่ยนสีตาม
            }
        }
    }

    void DropGhost()
    {
        Rigidbody2D rb = currentGhostInstance.GetComponent<Rigidbody2D>();
        if(rb != null) rb.simulated = true;
        
        currentGhostInstance = null; 
        StartCoroutine(WaitAndSpawnNext());
    }

    System.Collections.IEnumerator WaitAndSpawnNext()
    {
        canPlace = false;
        yield return new WaitForSeconds(nextDelay);
        SpawnNextInHand();
        canPlace = true;
    }
}