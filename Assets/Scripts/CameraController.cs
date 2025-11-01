using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector2 offset;

    void Update()
    {
        // Update the camera position based on the player's position and the offset
        if (player != null)
        {
            Vector3 newPosition = player.transform.position + new Vector3(offset.x, offset.y, -10);
            transform.position = newPosition;
        }
    }
}
