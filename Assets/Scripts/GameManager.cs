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

        Debug.Log("Game Over! Player has been caught.");

    }
}
