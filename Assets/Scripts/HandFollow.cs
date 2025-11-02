using UnityEngine;

public class HandFollow : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 2f;

    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - player.position;
        float distance = Mathf.Min(direction.magnitude, maxDistance);
        Vector3 targetPosition = player.position + direction.normalized * distance;

        transform.position = targetPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}