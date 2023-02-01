using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data for creating a MoneyEvent
/// </summary>
[CreateAssetMenu(fileName = "MoneyEventData", menuName = "ScriptableAssets/Monopoly/Events/Money")]
public class MoneyEventData : EventData
{
    /// <summary>
    /// Amount of money to give or remove to the instigator
    /// </summary>
    [SerializeField, Range(-5000, 5000)]
    int money = 0;

    /// <summary>
    /// Create a MoneyEvent
    /// </summary>
    /// <returns>A new MoneyEvent</returns>
    public override Event CreateEventFromData()
    {
        MoneyEvent _event = new MoneyEvent();
        _event.Money = money;
        _event.Data = this;
        return _event;
    }
}
