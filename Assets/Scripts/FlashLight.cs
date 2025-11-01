using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public float intensity = 1f;
    public bool isOn = false;

    [SerializeField] private FogController fogController; // REPLACE DIRECT REFERENCE

    private void Awake()
    {
        TurnOn();
    }

    public void TurnOn()
    {
        isOn = true;
        gameObject.SetActive(true);

        fogController.SetPlayerLights(true);
    }

    public void TurnOff()
    {
        isOn = false;
        gameObject.SetActive(false);

        fogController.SetPlayerLights(false);
    }
}
