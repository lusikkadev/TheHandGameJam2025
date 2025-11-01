using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransformLookAt : MonoBehaviour
{
    [SerializeField] private Transform toRotate;

    private void Update()
    {
        toRotate.LookAt(Input.mousePosition);
    }
}
