using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A cell that jailed instigator
/// </summary>
public class PrisonCell : Cell
{
    const string PRISON_CELL_BASE_TEXT = "Direction la prison pour";

    [SerializeField]
    int boardIndex = 10;

    [SerializeField, Range(1, 100)]
    int nbrTurn = 3;

    protected override string CellName => "Allez en prison !";

    public override void PlayCellEffect(MonopolyCharacter _instigator)
    {
        _instigator.TeleportTo(boardIndex);
        _instigator.TurnLeftInPrison = nbrTurn;
        PrintCellEffect($"{CellName} ! {PRISON_CELL_BASE_TEXT} {_instigator}", _instigator.Color);
        EndCellAction();
    }
}
