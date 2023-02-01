using UnityEngine;

/// <summary>
/// Editable data for monopoly player
/// </summary>
[CreateAssetMenu(fileName = "MonopolyCharacterData", menuName = "ScriptableAssets/Monopoly/Player")]
public class MonopolyCharacterData : ScriptableObject
{
    #region F/P

    /// <summary>
    /// Color of the sphere above character, useful for identifying which character is who
    /// </summary>
    [SerializeField]
    Color characterColor = Color.cyan;

    /// <summary>
    /// Name of the player
    /// </summary>
    [SerializeField]
    string characterName = "Player";

    /// <summary>
    /// Radius of the sphere
    /// </summary>
    [SerializeField, Range(0.001f, 1f)]
    float sphereRadius = 0.001f;

    public Color CharacterColor => characterColor;

    public string CharacterName => characterName;

    public float SphereRadius => sphereRadius;

    #endregion
}
