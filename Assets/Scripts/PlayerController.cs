using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 2f;                // 玩家移動速度（比敵人慢）
    public float dashSpeed = 5f;               // 衝刺速度
    public float dashDuration = 0.2f;          // 衝刺持續時間
    public float dashCooldown = 1f;            // 衝刺冷卻時間
    public GameObject soundWavePrefab;         // 聲音波紋的預製物件
    public float soundWaveCooldown = 0.5f;     // 聲音波紋生成的冷卻時間

    private float lastSoundWaveTime = 0f;
    private float lastDashTime = -Mathf.Infinity;
    private bool isDashing = false;
    private float dashEndTime = 0f;

    void Update()
    {
        // 玩家移動
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;

        if (isDashing)
        {
            // 在衝刺期間使用更高的速度
            transform.position += movement * dashSpeed * Time.deltaTime;

            if (Time.time >= dashEndTime)
            {
                isDashing = false; // 結束衝刺
            }
        }
        else
        {
            // 正常移動速度
            transform.position += movement * moveSpeed * Time.deltaTime;

            // 檢測衝刺按鍵
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
            {
                StartDash();
            }
        }

        // 生成聲音波紋
        if (movement.magnitude > 0 && Time.time - lastSoundWaveTime > soundWaveCooldown)
        {
            GenerateSoundWave();
            lastSoundWaveTime = Time.time;
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashEndTime = Time.time + dashDuration;
        lastDashTime = Time.time;

        // 播放衝刺音效（如果需要）
        // AudioManager.Instance.PlaySound(AudioManager.Instance.dashClip);
    }

    void GenerateSoundWave()
    {
        if (soundWavePrefab != null)
        {
            Instantiate(soundWavePrefab, transform.position, Quaternion.identity);

            // 播放聲音波紋生成音效
            AudioManager.Instance.PlaySound(AudioManager.Instance.soundWaveClip);
        }
        else
        {
            Debug.LogWarning("SoundWavePrefab is not assigned.");
        }
    }
}
