using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject gameOverPanel; // 遊戲結束的 UI 面板
    public Text gameOverText;        // 顯示遊戲結束訊息的文字

    private bool isGameOver = false;

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // 隱藏遊戲結束面板
        }
    }

    public void GameOver(string message)
    {
        if (isGameOver) return;

        isGameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // 顯示遊戲結束面板
        }

        if (gameOverText != null)
        {
            gameOverText.text = message; // 設定遊戲結束訊息
        }

        Time.timeScale = 0f; // 暫停遊戲
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // 恢復遊戲時間
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 重新加載場景
    }
}
