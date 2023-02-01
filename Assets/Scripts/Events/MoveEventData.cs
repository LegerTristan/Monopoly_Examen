using UnityEngine;

/// <summary>
/// Data for creating a MoveEvent
/// </summary>
[CreateAssetMenu(fileName = "MoveEventData", menuName = "ScriptableAssets/Monopoly/Events/Move")]
public class MoveEventData : EventData
{
    /// <summary>
    /// At which index the instigator needs to go
    /// </summary>
    [SerializeField]
    int boardIndex = 0;

    /// <summary>
    /// Defines if instigator can have the money gain by completing a turn.
    /// </summary>
    [SerializeField]
    bool canHaveStartMoney = true;

    /// <summary>
    /// Create a MoveEvent
    /// </summary>
    /// <returns>A new MoveEvent</returns>
    public override Event CreateEventFromData()
    {
        MoveEvent _event = new MoveEvent();
        _event.BoardIndex = boardIndex;
        _event.CanHaveStartMoney = canHaveStartMoney;
        _event.Data = this;
        return _event;
    }
}
