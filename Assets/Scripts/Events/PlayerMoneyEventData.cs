using UnityEngine;

/// <summary>
/// Data fro creating a PlayerMoneyEvent
/// </summary>
[CreateAssetMenu(fileName = "PlayerMoneyEventData", menuName = "ScriptableAssets/Monopoly/Events/PlayerMoney")]
public class PlayerMoneyEventData : EventData
{
    /// <summary>
    /// Amount of money each player has to give to instigator
    /// </summary>
    [SerializeField, Range(1, 10000)]
    int money = 100;

    /// <summary>
    /// Create a PlayerMoneyEvent
    /// </summary>
    /// <returns>Return a new PlayerMoneyEvent</returns>
    public override Event CreateEventFromData()
    {
        PlayerMoneyEvent _event = new PlayerMoneyEvent();
        _event.Amount = money;
        _event.Data = this;
        return _event;
    }
}
