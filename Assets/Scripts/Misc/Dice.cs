using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Dice of the monopoly. Returns a value clamped between 1 and 6
/// </summary>
public class Dice : SingletonTemplate<Dice>
{
    const int MAX_VALUE_DICE = 6;
    const int MIN_VALUE_DICE = 1;

    /// <summary>
    /// Invoked when a dice is rolled
    /// </summary>
    public event Action<int> OnDiceRolled = null;


    /// <summary>
    /// Returns a random dice value
    /// </summary>
    /// <returns>A value clamped between 1 and 6</returns>
    public int RollDice()
    {
        int _value = Random.Range(MIN_VALUE_DICE, MAX_VALUE_DICE + MIN_VALUE_DICE) ;
        OnDiceRolled?.Invoke(_value);
        return _value;
    }
}
