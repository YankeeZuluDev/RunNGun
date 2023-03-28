using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for event scriptable object
/// </summary>

[CreateAssetMenu(menuName = "Game Event")]

public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        // Iterate backwards to allow collection modification while iterating
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnRaise();
        }
    }

    public void Subscribe(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void Unsubscribe(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
