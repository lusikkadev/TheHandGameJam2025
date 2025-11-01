using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public float intensity = 1f;
    public bool isOn = false;

    public void TurnOn()
    {
        isOn = true;
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        isOn = false;
        gameObject.SetActive(false);

    }
}
