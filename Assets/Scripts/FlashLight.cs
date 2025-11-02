using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public float energy;
    public float maxEnergy = 20f;

    public bool isOn = false;

    [SerializeField] private FogController fogController; // REPLACE DIRECT REFERENCE
    [SerializeField] private Slider batterySlider;


    private Light2D[] lights;
    private Renderer[] renderers;
    private ParticleSystem[] particleSystems;

    TheHand theHand;

    private void Awake()
    {
        theHand = FindFirstObjectByType<TheHand>();
        batterySlider.maxValue = maxEnergy;
        lights = GetComponentsInChildren<Light2D>(true);
        renderers = GetComponentsInChildren<Renderer>(true);
        particleSystems = GetComponentsInChildren<ParticleSystem>(true);


        TurnOn();
    }



    private void Start()
    {
        energy = maxEnergy;

        batterySlider.maxValue = maxEnergy;
        batterySlider.minValue = 0f;
    }

    public void TurnOn()
    {

        isOn = true;
        SetVisualsActive(true);

        fogController?.SetPlayerLights(true);
    }

    public void TurnOff()
    {
        theHand.StartDescending();
        isOn = false;
        SetVisualsActive(false);

        fogController?.SetPlayerLights(false);
    }

    private void SetVisualsActive(bool value)
    {
        foreach (var l in lights) l.enabled = value;
        foreach (var r in renderers) r.enabled = value;

        foreach (var ps in particleSystems)
        {
            if (value) ps.Play();
            else ps.Stop();
        }
    }

    private void Update()
    {
        if (isOn)
        {
            energy -= 1f * Time.deltaTime;
            theHand.StopDescending();
            // auto turn off when drained
            if (energy <= 0f)
            {
                energy = 0f;
                TurnOff();
            }
        }
        else
        {
            energy += 0.5f * Time.deltaTime;
        }

        energy = Mathf.Clamp(energy, 0f, maxEnergy);
        batterySlider.value = energy;
    }
}