using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event that moves the instigator to wanted board index
/// </summary>
public class MoveEvent : Event
{
    /// <summary>
    /// Instigator of the event
    /// </summary>
    MonopolyCharacter tempInstigator = null;

    /// <summary>
    /// Board index to move instigator on
    /// </summary>
    public int BoardIndex { get; set; } = 0;

    /// <summary>
    /// Defines if instigator can gain money by completing a turn on the board
    /// </summary>
    public bool CanHaveStartMoney { get; set; } = true;

    /// <summary>
    /// Move the instigator to wanted cell and
    /// set if he can get money from a complete board turn
    /// /!\ Does not trigger OnEventEnded in order to move to wanted cell correctly
    /// </summary>
    /// <param name="_instigator">Instigator to apply event on</param>
    public override void PlayEvent(MonopolyCharacter _instigator)
    {
        if (BoardIndex < 0 || BoardIndex >= Board.BOARD_SIZE)
            return;

        tempInstigator = _instigator;
        int _nbrMove = BoardIndex - tempInstigator.CurrentBoardIndex;
        _nbrMove = _nbrMove < 0 ? _nbrMove + Board.BOARD_SIZE : _nbrMove;
        tempInstigator.MoveOnBoard(_nbrMove, CanHaveStartMoney);

        tempInstigator.OnBoardMoveEnded += HideEventCard;
    }

    /// <summary>
    /// Call HideEventCard from EventPanel and remove binding from instigators OnBoardMoveEnded 
    /// </summary>
    /// <param name="_index"></param>
    void HideEventCard(int _index)
    {
        MonopolyUIManager.Instance?.EventPanel?.HideEventCard();
        tempInstigator.OnBoardMoveEnded -= HideEventCard;
    }
}
