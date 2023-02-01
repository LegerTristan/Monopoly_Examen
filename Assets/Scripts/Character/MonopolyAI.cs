using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonopolyAI : MonopolyCharacter
{
    /// <summary>
    /// Chances that an AI buy a property.
    /// More the AI has properties, less chances it has to buy a property.
    /// Min chances clamped to 1/3
    /// </summary>
    protected int BuyPercentage
    {
        get
        {
            int _percentage = properties.Count / 2;
            return _percentage > 3 ? 3 : _percentage;
        }
    }
 
    /// <summary>
    /// Determine if an Ai wants to buy this property.
    /// </summary>
    /// <param name="_buyData"></param>
    public override void ChooseIfBuy(PropertyData _buyData)
    {
        bool _buy = Random.Range(0, BuyPercentage) == 0;

        ReturnChoiceIfBuy(_buy);
    }
}
