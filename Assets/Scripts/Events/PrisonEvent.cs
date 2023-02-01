
/// <summary>
/// Event that jailed the instigator at a specific cell
/// </summary>
public class PrisonEvent : Event
{
    /// <summary>
    /// Board index where instigator is jailed
    /// </summary>
    public int BoardIndex { get; set; } = 0;

    /// <summary>
    /// Number of turn instigator is jailed
    /// </summary>
    public int NbrTurn { get; set; } = 0;

    /// <summary>
    /// Teleport intigator to cell at board index and jailed him for 
    /// <see cref="NbrTurn"/>.
    /// </summary>
    /// <param name="_instigator">Character to apply event effect</param>
    public override void PlayEvent(MonopolyCharacter _instigator)
    {
        _instigator.TeleportTo(BoardIndex);
        _instigator.TurnLeftInPrison = NbrTurn;
        EndEvent();
    }
}
