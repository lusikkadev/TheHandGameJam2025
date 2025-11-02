using UnityEngine;

public class Boat : MonoBehaviour
{
    bool isMoving = false;
    GameObject player;  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Boat collided with " + collision.gameObject.name);
        isMoving = true;
        player = collision.gameObject;
        player.transform.SetParent(transform);

    }

    private void Update()
    {
        if (isMoving)
        {
            var position = transform.position;
            position.x += 1.0f * Time.deltaTime;
            transform.position = position;
        }
    }
}
