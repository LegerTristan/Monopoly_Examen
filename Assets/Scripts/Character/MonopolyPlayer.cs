using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A manipulable character by the user
/// </summary>
public class MonopolyPlayer : MonopolyCharacter
{
    /// <summary>
    /// Display confirmation panel and let the user choose if he wants to buy this property.
    /// </summary>
    /// <param name="_buyData">Data about the property</param>
    public override void ChooseIfBuy(PropertyData _buyData)
    {
        CardVisual.Instance?.PrintCardVisual(_buyData.BuyVisual);
        MonopolyUIManager.Instance?.ConfirmPanel?.PrintConfirmPanel(
            FormatBuyText(_buyData),
            () =>
            {
                ReturnChoiceIfBuy(true);
                CardVisual.Instance?.HideCardVisual();
            },
            () =>
            {
                ReturnChoiceIfBuy(false);
                CardVisual.Instance?.HideCardVisual();
            }
        );
    }

    string FormatBuyText(PropertyData _buyData)
    {
        return $"Voulez-vous acheter {_buyData.BuyName} pour {_buyData.BuyCost}" +
            $"{MonopolyGameManager.Instance?.Currency} ?";
    }
}
