using UnityEngine;

/// <summary>
/// Event cell that loads events from Community folder.
/// </summary>
public class CommunityEventCell : EventCell
{
    [SerializeField]
    string spritePath = "Sprites/Community";

    protected override string CellName => "Case communauté";

    protected override string EventDataPath => "Scriptables/Community";

    protected override string SpriteCardDataPath => spritePath;
}
