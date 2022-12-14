using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Event", menuName = "GameEvent")]
public class Event : ScriptableObject
{
    private List<EventListener> listeners = new List<EventListener>();

    // Add and remove from listeners list
    public void RegisterListener(EventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(EventListener listener)
    {
        listeners.Remove(listener);
    }

    // Simple Raise
    public void Raise()
    {
        if (listeners != null)
        {
            for (int i = listeners.Count-1; i>=0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }
    }
}