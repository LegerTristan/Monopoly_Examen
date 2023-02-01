using UnityEngine;

/// <summary>
/// EventCell that loads event from Luck folder
/// </summary>
public class LuckEventCell : EventCell
{
    [SerializeField]
    string spritePath = "Sprites/Chance";

    protected override string CellName => "Case chance";

    protected override string EventDataPath => "Scriptables/Luck";

    protected override string SpriteCardDataPath => spritePath;

}
