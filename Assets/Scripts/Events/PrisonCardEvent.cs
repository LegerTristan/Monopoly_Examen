/// <summary>
/// Play an event that give intigator the ability to leave prison 
/// without making a double next time.
/// </summary>
public class PrisonCardEvent : Event
{
    /// <summary>
    /// Give instigator the ability to leave prison without making a double next time.
    /// </summary>
    /// <param name="_instigator">Character to apply event effect</param>
    public override void PlayEvent(MonopolyCharacter _instigator)
    {
        _instigator.CanLeavePrison = true;
        EndEvent();
    }
}
