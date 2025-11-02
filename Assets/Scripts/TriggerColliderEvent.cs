using UnityEngine;
using UnityEngine.Events;

public class TriggerColliderEvent : MonoBehaviour
{
    public UnityEvent onTriggerStayEvent;
    public UnityEvent onTriggerExitEvent;

    private void OnTriggerStay2D(Collider2D other)
    {
        onTriggerStayEvent?.Invoke();
        Debug.Log("Trigger Stay Invoked");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        onTriggerExitEvent?.Invoke();
    }
}
