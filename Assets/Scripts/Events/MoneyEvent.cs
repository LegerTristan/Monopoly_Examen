using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event that increase or decrease money of the instigator
/// </summary>
public class MoneyEvent : Event
{
    /// <summary>
    /// Amount of money
    /// </summary>
    public int Money { get; set; } = 0;

    /// <summary>
    /// Increase or decrease money of the instigator
    /// </summary>
    /// <param name="_instigator">Character to apply event on</param>
    public override void PlayEvent(MonopolyCharacter _instigator)
    {
        _instigator.Money.Current += Money;
        EndEvent();
    }
}
