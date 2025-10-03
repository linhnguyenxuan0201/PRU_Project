using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject coinPrefab;           // Prefab của coin
    public Transform[] spawnPoints;         // Các điểm spawn coin
    public float spawnInterval = 3f;        // Khoảng thời gian giữa các lần spawn (giây)
    public int maxCoinsInScene = 10;        // Số coin tối đa trong scene
    public bool autoSpawn = true;           // Tự động spawn coin
    
    [Header("Random Spawn Settings")]
    public bool useRandomSpawn = false;     // Sử dụng spawn ngẫu nhiên
    public Vector2 spawnAreaMin;            // Góc trái dưới của khu vực spawn
    public Vector2 spawnAreaMax;            // Góc phải trên của khu vực spawn
    
    private float nextSpawnTime;
    private int currentCoinCount = 0;

    void Start()
    {
        // Thiết lập thời gian spawn đầu tiên
        nextSpawnTime = Time.time + spawnInterval;
        
        // Đếm số coin có sẵn trong scene
        CountExistingCoins();
    }

    void Update()
    {
        if (autoSpawn && Time.time >= nextSpawnTime && currentCoinCount < maxCoinsInScene)
        {
            SpawnCoin();
            nextSpawnTime = Time.time + spawnInterval;
        }
        
        // Cập nhật số coin hiện tại trong scene
        CountExistingCoins();
    }

    void SpawnCoin()
    {
        if (coinPrefab == null)
        {
            Debug.LogWarning("Coin Prefab is not assigned!");
            return;
        }

        Vector3 spawnPosition;

        if (useRandomSpawn)
        {
            // Spawn ngẫu nhiên trong khu vực được định nghĩa
            spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0f
            );
        }
        else
        {
            // Spawn tại các điểm được định nghĩa trước
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                spawnPosition = spawnPoints[randomIndex].position;
            }
            else
            {
                Debug.LogWarning("No spawn points assigned!");
                return;
            }
        }

        // Tạo coin tại vị trí spawn
        GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        
        // Đặt parent để tổ chức hierarchy
        newCoin.transform.SetParent(transform);
        
        Debug.Log($"Coin spawned at {spawnPosition}");
    }

    void CountExistingCoins()
    {
        // Đếm số coin hiện có trong scene
        currentCoinCount = FindObjectsOfType<Coin>().Length;
    }

    // Method để spawn coin thủ công
    public void ManualSpawnCoin()
    {
        if (currentCoinCount < maxCoinsInScene)
        {
            SpawnCoin();
        }
        else
        {
            Debug.Log("Maximum coins in scene reached!");
        }
    }

    // Method để spawn coin tại vị trí cụ thể
    public void SpawnCoinAtPosition(Vector3 position)
    {
        if (coinPrefab != null && currentCoinCount < maxCoinsInScene)
        {
            GameObject newCoin = Instantiate(coinPrefab, position, Quaternion.identity);
            newCoin.transform.SetParent(transform);
            Debug.Log($"Coin spawned at specific position: {position}");
        }
    }

    // Vẽ gizmos để hiển thị khu vực spawn trong Scene view
    void OnDrawGizmosSelected()
    {
        if (useRandomSpawn)
        {
            Gizmos.color = Color.yellow;
            Vector3 center = new Vector3(
                (spawnAreaMin.x + spawnAreaMax.x) / 2f,
                (spawnAreaMin.y + spawnAreaMax.y) / 2f,
                0f
            );
            Vector3 size = new Vector3(
                spawnAreaMax.x - spawnAreaMin.x,
                spawnAreaMax.y - spawnAreaMin.y,
                1f
            );
            Gizmos.DrawWireCube(center, size);
        }
        
        // Hiển thị spawn points
        if (spawnPoints != null)
        {
            Gizmos.color = Color.green;
            foreach (Transform point in spawnPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawWireSphere(point.position, 0.5f);
                }
            }
        }
    }
}