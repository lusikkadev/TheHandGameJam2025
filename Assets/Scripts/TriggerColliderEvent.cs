using UnityEngine;
using UnityEngine.Events;

public class TriggerColliderEvent : MonoBehaviour
{
    public UnityEvent onTriggerStayEvent;
    public UnityEvent onTriggerExitEvent;


    bool gotRef = false;
    PlayerReferences reference;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerStayEvent?.Invoke();
            if (!gotRef) 
            {
                reference = other.gameObject.GetComponent<PlayerReferences>();
            }
            
            if (reference != null) 
            {
                reference.GetFlashLight().ChangeLightRegenSpeed(true);
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            onTriggerExitEvent?.Invoke();

        if (reference != null)
        {
            reference.GetFlashLight().ChangeLightRegenSpeed(false);
        }
    }
}
