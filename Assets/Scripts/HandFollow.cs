using UnityEngine;

public class HandFollow : MonoBehaviour
{
    public Transform player;
    public float maxAngle = 20f; // Maximum angle in either direction from default

    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Direction from hand to mouse
        Vector3 direction = mousePosition - transform.position;

        // Calculate angle between player's right (forward) and mouse direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Clamp angle to [-maxAngle, maxAngle] relative to player's facing direction
        float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        // Apply rotation (assuming hand's default is pointing right)
        transform.rotation = Quaternion.Euler(0, 0, clampedAngle);
    }
}