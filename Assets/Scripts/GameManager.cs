using UnityEngine;

public class GameManager : MonoBehaviour
{

    public void GameOver()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        Debug.Log("Game Over! Player has been caught.");
    }
}
