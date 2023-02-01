using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event that add or remove money on instigator based on number of houses and hotels he have.
/// </summary>
public class HouseMoneyEvent : Event
{
    /// <summary>
    /// Money added or removed based on number of houses
    /// </summary>
    public int HouseMoney { get; set; } = 0;

    /// <summary>
    /// Money added or removed based on number of hotels
    /// </summary>
    public int HotelMoney { get; set; } = 0;

    /// <summary>
    /// Increase or decrease money of the instigator
    /// based on number of houses and hotels he have.
    /// </summary>
    /// <param name="_instigator">Character to apply event on</param>
    public override void PlayEvent(MonopolyCharacter _instigator)
    {
        int _nbrHouses = 0, _nbrHotels = 0;

        _instigator.GetNbrHousesAndHotels(ref _nbrHouses, ref _nbrHotels);
        _instigator.Money.Current += (HouseMoney * _nbrHouses) + (HotelMoney * _nbrHotels);
        EndEvent();
    }
}
