using UnityEngine;

/// <summary>
/// Basic settings for following a target
/// Include moveSpeed, smooth or constant speed and an offset.
/// </summary>
[CreateAssetMenu(fileName = "FollowSettings", menuName = "ScriptableAssets/Components/Follow")]
public class FollowSettings : ScriptableObject
{
    #region F/P

    /// <summary>
    /// Offset of the follow
    /// </summary>
    [SerializeField]
    Offset moveOffset = new Offset();

    /// <summary>
    /// Follow speed
    /// </summary>
    [SerializeField, Range(0.01f, 100f)]
    float moveSpeed = 1f;

    /// <summary>
    /// Define if owner can move to a target and if it moves smoothly or constantly
    /// </summary>
    [SerializeField]
    bool useMove = true, useSmoothMove = true;

    /// <summary>
    /// Color used for debugging
    /// </summary>
    [SerializeField, Header("Debug")]
    Color debugColor = Color.cyan;

    public Color DebugColor => debugColor;

    public float MoveSpeed => moveSpeed;

    public bool UseMove => useMove;

    public bool UseSmoothMove => useSmoothMove;

    #endregion

    #region Methods

    /// <summary>
    /// Returns offset applied for following target
    /// </summary>
    /// <param name="_origin">Origin of the offset, needed for local offset</param>
    /// <returns>Offset of the owner from its following target</returns>
    public Vector3 Offset(Transform _origin) => moveOffset.GetOffset(_origin);

    /// <summary>
    /// Returns offset applied for following target
    /// </summary>
    /// <param name="_rot">Rotation to apply, needed for local offset</param>
    /// <returns>Offset of the owner from its following target</returns>
    public Vector3 Offset(Quaternion _rot) => moveOffset.GetOffset(_rot);

    #endregion
}
