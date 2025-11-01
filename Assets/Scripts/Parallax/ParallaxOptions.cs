using UnityEngine;
using UnityEditor;

public class ParallaxOptions : MonoBehaviour
{
    public bool moveParallaxLayers;

    [SerializeField]
    [HideInInspector]
    private Vector3 storedPosition;

    [MenuItem("CameraControl/SavePosition")]
    static void SaveCameraPositionMenu()
    {
        Camera.main.GetComponent<ParallaxOptions>().SaveCameraPosition();
    }
    [MenuItem("CameraControl/LoadPosition")]
    static void LoadCameraPositionMenu()
    {
        Camera.main.GetComponent<ParallaxOptions>().LoadCameraPosition();
    }

    public void SaveCameraPosition()
    {
        storedPosition = transform.position;
        Debug.Log("Camera position saved: " + storedPosition);
    }

    public void LoadCameraPosition()
    {
        transform.position = storedPosition;
        Debug.Log("Camera position loaded: " + storedPosition);
    }

}