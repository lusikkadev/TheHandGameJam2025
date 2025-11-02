using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] private FlashLight playerFlashlight;

    public FlashLight GetFlashLight() 
    {
        return playerFlashlight;
    }
}
