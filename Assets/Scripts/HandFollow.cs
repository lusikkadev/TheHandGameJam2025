using UnityEngine;

public class HandFollow : MonoBehaviour
{
    public Transform player;
    public float maxAngle = 20f; // Maximum angle in either direction from default
    public bool facingRight = true;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        facingRight = player.localScale.x == 1;

        Vector3 direction;

        // Mirror mouse position if facing left
        if (facingRight)
        {
            direction = mousePosition - transform.position;
        }
        else
        {
            // Mirror the direction for left facing
            direction = transform.position - mousePosition;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Clamp to [-maxAngle, maxAngle] around 0 degrees
        float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, clampedAngle);
    }
}