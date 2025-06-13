using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject gameOverPanel; // 遊戲結束的 UI 面板
    public Text gameOverText;        // 顯示遊戲結束訊息的文字

    [Header("Pause Menu UI")]
    public GameObject pauseMenuPanel; // 暫停選單的 UI 面板

    private bool isGameOver = false;
    private bool isPaused = false;

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // 隱藏遊戲結束面板
        }

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false); // 隱藏暫停選單
        }
    }

    void Update()
    {
        // 檢測暫停按鍵
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
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

    public void PauseGame()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true); // 顯示暫停選單
        }

        Time.timeScale = 0f; // 暫停遊戲
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false); // 隱藏暫停選單
        }

        Time.timeScale = 1f; // 恢復遊戲
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // 恢復遊戲時間
        SceneManager.LoadScene("MainMenuScene"); // 返回主選單場景
    }
}
