using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // 加載遊戲場景，確保場景名稱正確
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // 退出遊戲（僅在打包後有效）
    }
}
