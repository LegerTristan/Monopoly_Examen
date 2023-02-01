using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cell thats gives an equivalant amount of money gain by turn complete when instigator lands on
/// </summary>
public class StartCell : Cell
{
    protected override string CellName => "Case départ";

    public override void PlayCellEffect(MonopolyCharacter _instigator)
    {
        MonopolyGameManager _instance = MonopolyGameManager.Instance;

        int _moneyOffered = _instance ? _instance.MoneyGainedByTurnComplete: 0;

        _instigator.Money.Current += _moneyOffered;
        PrintCellEffect($"{CellName} : {_instigator} gagne {_moneyOffered}" +
            $"{MonopolyGameManager.Instance?.Currency} en plus !",
            _instigator.Color);

        EndCellAction();
    }
}
