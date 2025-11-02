using UnityEngine;

public class TransformFollow : MonoBehaviour
{
    [SerializeField] private Transform toFollow;
    [SerializeField] private Vector3 offset;

    bool follow = true;

    private void Update()
    {
        if (follow)
        {
            transform.position = toFollow.position + offset;
        }
    }
    public void StopFollow() 
    {
        follow = false;
    }
}
