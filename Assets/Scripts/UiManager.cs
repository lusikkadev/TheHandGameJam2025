using UnityEngine;
using UnityEngine.SceneManagement;
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
        SceneManager.LoadScene(0);
    }
    private void RestartGame() 
    {
        SceneManager.LoadScene(1);
    }
}
