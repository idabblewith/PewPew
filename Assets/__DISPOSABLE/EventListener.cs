using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public Event GameEvent;
    public UnityEvent Response;

    // Register and deregister self from Event
    private void OnEnable() {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable() {
        GameEvent.UnregisterListener(this);
    }

    // What happens when event occurs
    public void OnEventRaised()
    {
        Response?.Invoke();
    }
}
