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

    [Header("Score UI")]
    public Text scoreText; // 顯示分數的 UI 元素

    private bool isGameOver = false;
    private bool isPaused = false;
    private int score = 0; // 玩家分數
    private bool isScoring = true;
    private int highScore = 0; // 歷史最高分數

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

        if (scoreText != null)
        {
            scoreText.text = "Score: 0"; // 初始化分數顯示
        }

        // 從 PlayerPrefs 加載最高分數
        highScore = PlayerPrefs.GetInt("HighScore", 0);
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

        // 持續增加分數（例如根據存活時間）
        if (isScoring)
        {
            score += Mathf.FloorToInt(Time.deltaTime * 10); // 每秒增加 10 分
            UpdateScoreUI();
        }
    }

    public void GameOver(string message)
    {
        if (isGameOver) return;

        isGameOver = true;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // 儲存最高分數
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // 顯示遊戲結束面板
        }

        if (gameOverText != null)
        {
            gameOverText.text = message + "\nFinal Score: " + score + "\nHigh Score: " + highScore; // 顯示最終分數和最高分數
        }

        Time.timeScale = 0f; // 暫停遊戲
        StopScoring();
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

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void StopScoring()
    {
        isScoring = false; // 停止計分
    }

    public int GetScore()
    {
        return score; // 返回當前分數
    }
}
