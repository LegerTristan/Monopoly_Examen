using UnityEngine;

/// <summary>
/// Contains data to create a PrisonEvent.
/// </summary>
[CreateAssetMenu(fileName = "PrisonEventData", menuName = "ScriptableAssets/Monopoly/Events/Prison")]

public sealed class PrisonEventData : EventData
{
    /// <summary>
    /// At which board index we need to teleport the instigator
    /// </summary>
    [SerializeField]
    int boardIndex = 10;

    /// <summary>
    /// Number of turn the instigator is jailed
    /// </summary>
    [SerializeField, Range(1, 100)]
    int nbrTurn = 3;

    /// <summary>
    /// Create a PrisonEvent
    /// </summary>
    /// <returns>Return a new Prison Event</returns>
    public override Event CreateEventFromData()
    {
        PrisonEvent _event = new PrisonEvent();
        _event.BoardIndex = boardIndex;
        _event.NbrTurn = nbrTurn;
        _event.Data = this;
        return _event;
    }
}
