using UnityEngine;

/// <summary>
/// Abstract class. Scriptable that contains data for event
/// Can create an event from data it contains.
/// </summary>
public abstract class EventData : ScriptableObject
{
    /// <summary>
    /// Text of the event
    /// </summary>
    [SerializeField]
    string eventText = null;

    /// <summary>
    /// Getter for eventText
    /// </summary>
    public string EventText => eventText;

    /// <summary>
    /// Create an Event from the data contained in this class
    /// </summary>
    /// <returns></returns>
    public abstract Event CreateEventFromData();
}
