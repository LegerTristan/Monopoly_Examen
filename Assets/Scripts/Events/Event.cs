using System;
using UnityEngine;

/// <summary>
/// Base class for all events.
/// Contains an EventData and trigger OnEventEnded when event is completed.
/// </summary>
public abstract class Event
{
    #region F/P
    /// <summary>
    /// Trigger when event effect ended
    /// </summary>
    public event Action OnEventEnded = null;

    /// <summary>
    /// Data of the event
    /// </summary>
    public EventData Data { get; set; } = null;
    #endregion

    #region Methods
    /// <summary>
    /// Play event effect on instigator
    /// </summary>
    /// <param name="_instigator">Character that has triggered the event</param>
    public abstract void PlayEvent(MonopolyCharacter _instigator);

    /// <summary>
    /// Invoke OnEventEnded, use by child classes to trigger OnEventEnded.
    /// </summary>
    protected void EndEvent() => OnEventEnded?.Invoke();
    #endregion
}
