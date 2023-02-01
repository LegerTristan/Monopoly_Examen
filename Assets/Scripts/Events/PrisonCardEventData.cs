using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data for creating a PrisonCardEvent
/// </summary>
[CreateAssetMenu(fileName = "PrisonCardEventData", menuName = "ScriptableAssets/Monopoly/Events/PrisonCard")]
public class PrisonCardEventData : EventData
{
    /// <summary>
    /// Create a new PrisonCardEvent
    /// </summary>
    /// <returns>A new PrisonCardEvent</returns>
    public override Event CreateEventFromData()
    {
        PrisonCardEvent _event = new PrisonCardEvent();
        _event.Data = this;
        return _event;
    }
}
