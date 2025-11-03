using UnityEngine;

public class HandFollow : MonoBehaviour
{
    public Transform player;
    public float maxAngle = 45f; // max angle
    public bool facingRight = true;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        facingRight = player.localScale.x == 1;

        Vector3 direction;

       
        // hahmon flippaus inverted aim
        if (facingRight)
        {
            direction = mousePosition - transform.position;
        }
        else
        {
            
            direction = transform.position - mousePosition;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, clampedAngle);
    }
}