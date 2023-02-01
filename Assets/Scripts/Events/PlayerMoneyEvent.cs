using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event that give to instigator an amount of money based on other players money
/// </summary>
public class PlayerMoneyEvent : Event
{
    /// <summary>
    /// Amount of money to pick from otther players
    /// </summary>
    public int Amount { get; set; } = 0;

    /// <summary>
    /// Take the amount of money to each other player and give it to instigator
    /// </summary>
    /// <param name="_instigator">Character to apply event effect on</param>
    public override void PlayEvent(MonopolyCharacter _instigator)
    {
        MonopolyCharacterManager _characterManager = MonopolyGameManager.Instance?.CharacterManager;

        int _total = 0;

        for (int i = 0; i < _characterManager.Count; i++)
        {
            if (_characterManager[i] && _characterManager[i].CanAffordCost(Amount))
            {
                _total += Amount;
                _characterManager[i].Money.Current -= Amount;
            }
        }
        _instigator.Money.Current += _total;
        EndEvent();
    }
}
