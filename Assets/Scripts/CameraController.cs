using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector2 offset;
    public float maxYFollow = 0.5f;

    private float baseY;

    void Start()
    {

        baseY = transform.position.y;
    }

    void Update()
    {
        if (player != null)
        {
       
            float targetY = player.transform.position.y + offset.y;
         
            float clampedY = Mathf.Clamp(targetY, baseY - maxYFollow, baseY + maxYFollow);

            Vector3 newPosition = new Vector3(player.transform.position.x + offset.x, clampedY, -10);
            transform.position = newPosition;
        }
    }
}
