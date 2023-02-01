using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cell that decrease money of instigator when he lands on.
/// </summary>
public class TaxCell : Cell
{
    [SerializeField, Range(1, 2500)]
    public int moneyTaxed = 500;

    protected override string CellName => "Case taxe";

    public override void PlayCellEffect(MonopolyCharacter _instigator)
    {
        _instigator.Money.Current -= moneyTaxed;
        PrintCellEffect($"{CellName} : {_instigator} perd {moneyTaxed}" +
            $"{MonopolyGameManager.Instance?.Currency}", _instigator.Color);

        EndCellAction();
    }
}
