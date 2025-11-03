using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UiManager uiManager;
    private FogController fogController;
    private void Awake()
    {
        uiManager = FindFirstObjectByType<UiManager>();
        fogController = FindFirstObjectByType<FogController>();
    }
    public void GameOver()
    {
        fogController.OnPlayerGrabbed();
        fogController.StopFogFollow();

        StartCoroutine(GameOverCo());
    }

    private IEnumerator GameOverCo() 
    {
        yield return new WaitForSeconds(2.5f);

        uiManager.OnGameOver();
    }

    public void RestartThisScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
