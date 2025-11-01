using UnityEngine;
using UnityEngine.Events;

public class TriggerColliderEvent : MonoBehaviour
{
    public UnityEvent onTriggerEnterEvent;
    public UnityEvent onTriggerExitEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnterEvent?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        onTriggerExitEvent?.Invoke();
    }
}
