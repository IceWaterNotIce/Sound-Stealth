using UnityEngine;

public class SoundWaveController : MonoBehaviour
{
    [Header("Sound Wave Settings")]
    public AnimationCurve expansionCurve; // 控制波紋擴散速度的曲線
    public float maxRadius = 5f;          // 波紋的最大半徑
    public float duration = 2f;           // 波紋持續時間

    private CircleCollider2D circleCollider;
    private float elapsedTime = 0f;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider == null)
        {
            Debug.LogError("CircleCollider2D is required on the GameObject.");
        }
    }

    void Update()
    {
        if (circleCollider == null) return;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > duration)
        {
            Destroy(gameObject); // 波紋結束後銷毀物件
            return;
        }

        // 根據 Animation Curve 計算當前半徑
        float normalizedTime = elapsedTime / duration;
        float currentRadius = expansionCurve.Evaluate(normalizedTime) * maxRadius;

        // 更新 Circle Collider 的半徑
        circleCollider.radius = currentRadius;
    }
}
