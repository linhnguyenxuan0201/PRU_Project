using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI coinCountText;   // Text hiển thị số coin (TextMeshPro)
    public Text coinCountTextLegacy;        // Text hiển thị số coin (Legacy Text)
    public Image coinIcon;                  // Icon coin trên UI
    
    [Header("Animation Settings")]
    public bool animateOnCollect = true;    // Animate khi nhặt coin
    public float animationDuration = 0.3f;  // Thời gian animation
    public float scaleFactor = 1.2f;        // Tỷ lệ scale khi animate
    
    private Player player;
    private int lastCoinCount = 0;
    private Vector3 originalScale;

    void Start()
    {
        // Tìm Player trong scene
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogWarning("Player not found in scene!");
        }

        // Lưu scale gốc cho animation
        if (coinIcon != null)
        {
            originalScale = coinIcon.transform.localScale;
        }

        // Cập nhật UI lần đầu
        UpdateCoinDisplay();
    }

    void Update()
    {
        // Cập nhật UI khi số coin thay đổi
        if (player != null)
        {
            int currentCoins = player.GetTotalCoins();
            if (currentCoins != lastCoinCount)
            {
                UpdateCoinDisplay();
                
                // Animate nếu có thu thập coin mới
                if (animateOnCollect && currentCoins > lastCoinCount)
                {
                    AnimateCoinIcon();
                }
                
                lastCoinCount = currentCoins;
            }
        }
    }

    void UpdateCoinDisplay()
    {
        if (player == null) return;

        int coinCount = player.GetTotalCoins();
        string coinText = coinCount.ToString();

        // Cập nhật TextMeshPro nếu có
        if (coinCountText != null)
        {
            coinCountText.text = coinText;
        }

        // Cập nhật Legacy Text nếu có
        if (coinCountTextLegacy != null)
        {
            coinCountTextLegacy.text = coinText;
        }
    }

    void AnimateCoinIcon()
    {
        if (coinIcon != null)
        {
            // Sử dụng LeanTween hoặc tạo animation đơn giản
            StartCoroutine(ScaleAnimation());
        }
    }

    System.Collections.IEnumerator ScaleAnimation()
    {
        if (coinIcon == null) yield break;

        float elapsedTime = 0f;
        Vector3 targetScale = originalScale * scaleFactor;

        // Scale up
        while (elapsedTime < animationDuration / 2f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (animationDuration / 2f);
            coinIcon.transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            yield return null;
        }

        elapsedTime = 0f;

        // Scale down
        while (elapsedTime < animationDuration / 2f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (animationDuration / 2f);
            coinIcon.transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);
            yield return null;
        }

        // Đảm bảo scale về đúng giá trị gốc
        coinIcon.transform.localScale = originalScale;
    }

    // Method để thiết lập references từ code
    public void SetCoinText(TextMeshProUGUI textMeshPro)
    {
        coinCountText = textMeshPro;
    }

    public void SetCoinTextLegacy(Text legacyText)
    {
        coinCountTextLegacy = legacyText;
    }

    public void SetCoinIcon(Image icon)
    {
        coinIcon = icon;
        if (icon != null)
        {
            originalScale = icon.transform.localScale;
        }
    }

    // Method để force update UI
    public void ForceUpdateDisplay()
    {
        UpdateCoinDisplay();
    }
}