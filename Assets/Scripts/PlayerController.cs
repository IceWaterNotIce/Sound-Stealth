using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 3f;                // 玩家移動速度
    public GameObject soundWavePrefab;         // 聲音波紋的預製物件
    public float soundWaveCooldown = 0.5f;     // 聲音波紋生成的冷卻時間

    private float lastSoundWaveTime = 0f;

    void Update()
    {
        // 玩家移動
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;

        // 生成聲音波紋
        if (movement.magnitude > 0 && Time.time - lastSoundWaveTime > soundWaveCooldown)
        {
            GenerateSoundWave();
            lastSoundWaveTime = Time.time;
        }
    }

    void GenerateSoundWave()
    {
        if (soundWavePrefab != null)
        {
            Instantiate(soundWavePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("SoundWavePrefab is not assigned.");
        }
    }
}
