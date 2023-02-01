using UnityEngine;

/// <summary>
/// Data for creating a HouseMoneyEvent
/// </summary>
[CreateAssetMenu(fileName = "HouseMoneyEventData", menuName = "ScriptableAssets/Monopoly/Events/HouseMoney")]

public class HouseMoneyEventData : EventData
{
    /// <summary>
    /// Money added or removed by house and hotel.
    /// </summary>
    [SerializeField, Range(-5000, 5000)]
    int houseMoney = 0, hotelMoney = 0;

    /// <summary>
    /// Create a HouseMoneyEvent
    /// </summary>
    /// <returns>A new HouseMoneyEvent</returns>
    public override Event CreateEventFromData()
    {
        HouseMoneyEvent _event = new HouseMoneyEvent();
        _event.HouseMoney = houseMoney;
        _event.HotelMoney = hotelMoney;
        _event.Data = this;
        return _event;
    }
}
