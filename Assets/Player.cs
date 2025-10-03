using UnityEngine;
using UnityEngine.InputSystem; // Input System mới

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    public Animator Animator;
    public Rigidbody2D rb;
    public float jumpHeight = 5f;
    public bool isGround = true;
    private float movement;
    public float moveSpeed = 5f;
    private bool facingRight = true;
    
    [Header("Coin Collection")]
    public int totalCoins = 0;          // Tổng số coin đã nhặt
    public AudioClip coinCollectSound;  // Âm thanh khi nhặt coin
    
    private AudioSource audioSource;

    void Start()
    {
        rb.transform.localScale = new Vector3(1, 1, 1);
        
        // Lấy hoặc thêm AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Lấy input trái/phải từ bàn phím
        float left = Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed ? -1f : 0f;
        float right = Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed ? 1f : 0f;
        movement = left + right;

        // Debug giá trị ngang
        Debug.Log("Horizontal: " + movement);

        // Xoay nhân vật theo hướng di chuyển
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        else if (movement > 0f && !facingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }

        // Nhảy bằng phím Space
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGround)
        {
            Jump();
            isGround = false;
            Animator.SetBool("Jump", true);
        }

        // Animation chạy
        if (Mathf.Abs(movement) > .1f)
        {
            Animator.SetFloat("Run", 1f);
        }
        else
        {
            Animator.SetFloat("Run", 0f);
        }

        // Attack bằng chuột trái (dùng Input System mới)
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        // Di chuyển ngang
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            Animator.SetBool("Jump", false);
        }
    }

    // Method để thu thập coin (được gọi từ Coin script)
    public void CollectCoin(int coinValue)
    {
        totalCoins += coinValue;
        
        // Debug thông tin coin
        Debug.Log($"Coin collected! Value: {coinValue}, Total coins: {totalCoins}");

        // Phát âm thanh nếu có
        if (coinCollectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(coinCollectSound);
        }

        // Có thể thêm animation hoặc hiệu ứng khác ở đây
        // Ví dụ: trigger animation thu thập
        if (Animator != null)
        {
            // Animator.SetTrigger("CollectCoin"); // Uncomment nếu có animation
        }
    }

    // Method để lấy số coin hiện tại (để hiển thị UI)
    public int GetTotalCoins()
    {
        return totalCoins;
    }

    // Method để thiết lập số coin (để load game hoặc cheat)
    public void SetTotalCoins(int amount)
    {
        totalCoins = amount;
        Debug.Log($"Total coins set to: {totalCoins}");
    }

    // Method để sử dụng coin (mua đồ, upgrade, etc.)
    public bool SpendCoins(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            Debug.Log($"Spent {amount} coins. Remaining: {totalCoins}");
            return true;
        }
        else
        {
            Debug.Log($"Not enough coins! Need {amount}, have {totalCoins}");
            return false;
        }
    }
}
