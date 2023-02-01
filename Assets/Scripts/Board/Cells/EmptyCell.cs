using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cell that does nothing when instigator lands on.
/// </summary>
public class EmptyCell : Cell
{
    protected override string CellName => "Simple visite";

    public override void PlayCellEffect(MonopolyCharacter _instigator)
    {
        EndCellAction();
    }
}
