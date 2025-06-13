using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Clips")]
    public AudioClip soundWaveClip;    // 聲音波紋生成音效
    public AudioClip playerCaughtClip; // 玩家被抓到音效
    public AudioClip enemyDetectClip;  // 敵人偵測到玩家音效
    public AudioClip dashClip;         // 衝刺音效

    private AudioSource audioSource;

    void Awake()
    {
        // 確保 AudioManager 是單例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
