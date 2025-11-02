using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UiManager uiManager;
    private void Awake()
    {
        uiManager = FindFirstObjectByType<UiManager>();
    }
    public void GameOver()
    {
        uiManager.OnGameOver();
    }
}
