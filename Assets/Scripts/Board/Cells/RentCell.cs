using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A cell taht can be owned and has a rent when other players lands on on.
/// </summary>
public abstract class RentCell : Cell
{
    #region F/P
    protected MonopolyCharacter tempInstigator = null;

    protected abstract MonopolyCharacter PropertyOwner { get; }

    protected abstract int Cost { get; }

    protected abstract int Rent { get; }

    protected abstract bool IsRentCellValid { get; }
    #endregion 

    void Start() => InitRentCell();

    #region CustomMethods
    protected virtual void InitRentCell()
    {
        if (!IsRentCellValid)
            return;
    }

    public override void PlayCellEffect(MonopolyCharacter _instigator)
    {
        //_instigator.IgnoreEndCellAction = true;
        PlayRentCellEffect(_instigator);
    }

    protected abstract void PlayRentCellEffect(MonopolyCharacter _instigator);

    protected virtual void ApplyRent()
    {
        tempInstigator.Money.Current -= Rent;
        PropertyOwner.Money.Current += Rent;
        PrintCellEffect($"{tempInstigator} paye un loyer de {Rent}" +
            $"{MonopolyGameManager.Instance?.Currency} à {PropertyOwner}", tempInstigator.Color);
        EndCellAction();
    }

    protected virtual void ProposeToBuy()
    {
        if (!tempInstigator.CanAffordCost(Cost))
        {
            PrintCellEffect($"{tempInstigator} ne peut pas acheter {CellName} !", tempInstigator.Color);
            EndCellAction();
            return;
        }

        tempInstigator.OnPropertyBoughtChoiceMade += CheckInstigatorChoice;
    }

    protected virtual void CheckInstigatorChoice(bool _result)
    {
        if (_result)
            GiveProperty();
        else
            PrintCellEffect($"{tempInstigator} n'achète pas {CellName} !", tempInstigator.Color);

        tempInstigator.OnPropertyBoughtChoiceMade -= CheckInstigatorChoice;

        EndCellAction();
    }

    protected abstract void GiveProperty();
    #endregion
}
