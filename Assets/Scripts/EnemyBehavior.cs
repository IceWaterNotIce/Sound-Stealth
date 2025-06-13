using UnityEngine;
using UnityEngine.SceneManagement; // 用於重新加載場景

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float detectionRadius = 5f; // 偵測範圍
    public float moveSpeed = 2f;       // 移動速度

    [Header("Patrol Settings")]
    public float patrolRadius = 3f;    // 巡邏範圍
    public float patrolSpeed = 1.5f;   // 巡邏速度
    public float patrolWaitTime = 2f; // 每次巡邏等待時間

    [Header("Player Detection")]
    public string playerTag = "Player"; // 玩家物件的 Tag

    private Transform target;          // 玩家目標
    private bool isChasing = false;    // 是否正在追蹤玩家
    private Vector3 patrolTarget;      // 巡邏目標位置
    private bool isPatrolling = true;  // 是否正在巡邏
    private float patrolTimer = 0f;

    void Start()
    {
        SetNewPatrolTarget();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 偵測到聲音波紋
        if (other.CompareTag("SoundWave"))
        {
            Vector3 soundPosition = other.transform.position;
            if (Vector3.Distance(transform.position, soundPosition) <= detectionRadius)
            {
                // 設定目標為聲音波紋的位置
                target = other.transform;
                isChasing = true;
                isPatrolling = false;

                // 播放敵人偵測到聲音的音效
                AudioManager.Instance.PlaySound(AudioManager.Instance.enemyDetectClip);
            }
        }

        // 偵測到玩家
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player caught! Game Over.");
            AudioManager.Instance.PlaySound(AudioManager.Instance.playerCaughtClip); // 播放玩家被抓到的音效
            FindObjectOfType<GameManager>().GameOver("You were caught by the enemy!");
        }
    }

    void Update()
    {
        if (isChasing && target != null)
        {
            // 朝目標移動
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 停止追蹤當接近目標時
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                isChasing = false;
                target = null;
                isPatrolling = true;
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolTarget) < 0.1f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                SetNewPatrolTarget();
                patrolTimer = 0f;
            }
        }
        else
        {
            Vector3 direction = (patrolTarget - transform.position).normalized;
            transform.position += direction * patrolSpeed * Time.deltaTime;
        }
    }

    void SetNewPatrolTarget()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized * patrolRadius;
        patrolTarget = new Vector3(transform.position.x + randomDirection.x, transform.position.y + randomDirection.y, transform.position.z);
    }
}
