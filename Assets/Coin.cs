using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin Settings")]
    public int coinValue = 1;           // Giá trị của coin
    public float rotationSpeed = 90f;   // Tốc độ xoay (độ/giây)
    public AudioClip collectSound;      // Âm thanh khi nhặt coin (tùy chọn)
    
    [Header("Effects")]
    public GameObject collectEffect;    // Hiệu ứng particle khi nhặt (tùy chọn)
    
    private AudioSource audioSource;

    void Start()
    {
        // Lấy AudioSource component nếu có
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Xoay coin liên tục quanh trục Y để tạo hiệu ứng đẹp mắt
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    // Xử lý khi player chạm vào coin (sử dụng Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem object va chạm có phải là Player không
        if (other.CompareTag("Player"))
        {
            // Lấy component Player từ object
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // Thêm coin vào inventory của player
                player.CollectCoin(coinValue);
                
                // Phát âm thanh nếu có
                PlayCollectSound();
                
                // Tạo hiệu ứng nếu có
                CreateCollectEffect();
                
                // Hủy coin object
                Destroy(gameObject);
            }
        }
    }

    void PlayCollectSound()
    {
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }

    void CreateCollectEffect()
    {
        if (collectEffect != null)
        {
            // Tạo hiệu ứng tại vị trí coin
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
    }

    // Method để thiết lập giá trị coin từ bên ngoài
    public void SetCoinValue(int value)
    {
        coinValue = value;
    }
}