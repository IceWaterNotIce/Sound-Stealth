using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel; // 遊戲說明的 UI 面板

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // 加載遊戲場景，確保場景名稱正確
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // 退出遊戲（僅在打包後有效）
    }

    public void ShowHelp()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(true); // 顯示遊戲說明面板
        }
    }

    public void HideHelp()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(false); // 隱藏遊戲說明面板
        }
    }
}
