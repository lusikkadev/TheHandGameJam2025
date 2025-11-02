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
<<<<<<< HEAD
        uiManager.OnGameOver();
=======
        Debug.Log("Game Over! Player has been caught.");
>>>>>>> b58e91870de4b17ba0cc8206772c504a0a40ac30
    }
}
