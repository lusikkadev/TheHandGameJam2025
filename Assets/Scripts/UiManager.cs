using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        quitButton.onClick.AddListener(() =>
        {
            QuitGame();
        });
        restartButton.onClick.AddListener(() => 
        {
            RestartGame();
        });

        gameOverPanel.SetActive(false);
    }
    public void OnGameOver() 
    {
        gameOverPanel.SetActive(true);
    }
    private void QuitGame() 
    {
        Application.Quit();
    }
    private void RestartGame() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
