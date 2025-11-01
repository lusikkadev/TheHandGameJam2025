using UnityEngine;

public class TransformFollow : MonoBehaviour
{
    [SerializeField] private Transform toFollow;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        transform.position = new Vector3(toFollow.position.x + offset.x, 0f, 0f);
    }
}
