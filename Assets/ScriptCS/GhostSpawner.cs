using UnityEngine;
using UnityEngine.UI;

public class GhostSpawner : MonoBehaviour
{
    [Header("Ghost Prefabs")]
    public GameObject ghostLV1;
    public GameObject ghostLV2;

    [Header("UI Reference")]
    public Image nextGhostUI; // ลาก Image UI มุมจอมาใส่ที่นี่
    public Sprite spriteLV1;  // ลากรูปผีขาวมาใส่
    public Sprite spriteLV2;  // ลากรูปผีแดงมาใส่

    [Header("Settings")]
    public float spawnY = 4f;
    public float spawnRangeX = 2.5f;
    public float nextDelay = 0.5f;

    private GameObject currentGhostInstance; // ตัวที่ค้างที่เมาส์
    private int nextGhostLevel; // เก็บค่าว่าตัวต่อไปจะเป็น LV อะไร
    private bool canPlace = true;

    void Start()
    {
        // เริ่มเกม: สุ่มตัวถัดไปก่อน แล้วค่อยสร้างตัวปัจจุบัน
        nextGhostLevel = Random.Range(1, 3); 
        SpawnNextInHand();
    }

    void Update()
    {
        // 1. ให้ตัวในมือเลื่อนตามเมาส์ (แต่ยังไม่ปล่อย)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(mousePos.x, -spawnRangeX, spawnRangeX);
        transform.position = new Vector3(clampedX, spawnY, 0);

        if (currentGhostInstance != null)
        {
            currentGhostInstance.transform.position = transform.position;
        }

        // 2. คลิกเพื่อปล่อย
        if (Input.GetMouseButtonDown(0) && canPlace && currentGhostInstance != null)
        {
            DropGhost();
        }
    }

    void SpawnNextInHand()
    {
        // สร้างผีในมือตามระดับที่สุ่มไว้ก่อนหน้า
        GameObject prefabToSpawn = (nextGhostLevel == 1) ? ghostLV1 : ghostLV2;
        currentGhostInstance = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        
        // ปิดฟิสิกส์ชั่วคราวขณะอยู่ในมือ (ไม่ให้มันหล่นหรือชนใคร)
        Rigidbody2D rb = currentGhostInstance.GetComponent<Rigidbody2D>();
        rb.simulated = false;

        // สุ่มตัว "ถัดไป" สำหรับคิวหน้า และอัปเดต UI
        nextGhostLevel = Random.Range(1, 3);
        UpdateNextUI();
    }

    void UpdateNextUI()
    {
        // เปลี่ยนรูปที่มุมจอตามระดับที่สุ่มได้
        nextGhostUI.sprite = (nextGhostLevel == 1) ? spriteLV1 : spriteLV2;
    }

    void DropGhost()
    {
        // เปิดฟิสิกส์เพื่อให้มันร่วงลงหม้อ
        Rigidbody2D rb = currentGhostInstance.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        
        currentGhostInstance = null; // ปล่อยการควบคุม
        
        // รอดีเลย์แป๊บหนึ่งก่อนเอาตัวถัดไปขึ้นมาบนมือ
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