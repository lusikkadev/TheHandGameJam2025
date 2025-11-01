using UnityEngine;

public class TransformFollow : MonoBehaviour
{
    [SerializeField] private Transform toFollow;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        transform.position = toFollow.position + offset;
    }
}
