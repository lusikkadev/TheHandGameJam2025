using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            RestartGame();
        });
        quitButton.onClick.AddListener(() =>
        {
            QuitGame();
        });
        gameOverPanel.SetActive(false);
    }
    public void OnGameOver() 
    {
        gameOverPanel.SetActive(true);
    }

    private void RestartGame() 
    {
        SceneManager.LoadScene(0);
    }
    private void QuitGame() 
    {
        Application.Quit();
    }

}
