using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UPGRADE => Add damping

/// <summary>
/// Basic settings used for personalize cameras look at feature.
/// </summary>
[CreateAssetMenu(fileName = "LookAtSettings", menuName = "ScriptableAssets/Camera/LookAt")]
public class LookAtSettings : ScriptableObject
{
    #region F/P

    /// <summary>
    /// Offset of the look at
    /// </summary>
    [SerializeField]
    Offset lookAtOffset = new Offset();

    /// <summary>
    /// Look at speed, range between 0.1f and 100f
    /// </summary>
    [SerializeField, Range(0.1f, 100f)]
    float lookAtSpeed = 1f;

    /// <summary>
    /// Defines if follower look at target or if it looks at smoothly or constantly
    /// Defines also if follower stop look at when it reaches target
    /// </summary>
    [SerializeField]
    bool useLookAt = true, useSmoothLookAt = true, stopLookAtWhenMoveStop = false;

    /// <summary>
    /// Color for debugging
    /// </summary>
    [SerializeField, Header("Debug")]
    Color debugColor = Color.magenta;

    public Color DebugColor => debugColor;

    public float LookAtSpeed => lookAtSpeed;

    public bool UseLookAt => useLookAt;

    public bool UseSmoothLookAt => useSmoothLookAt;

    public bool StopLookAtWhenMoveStop => stopLookAtWhenMoveStop;
    #endregion


    #region Methods
    /// <summary>
    /// Returns offset applied to look at
    /// </summary>
    /// <param name="_origin">Origin of the offset</param>
    /// <returns>Offset in world or local space</returns>
    public Vector3 Offset(Transform _origin) => lookAtOffset.GetOffset(_origin);

    /// <summary>
    /// Returns offset applied to look at
    /// </summary>
    /// <param name="_rot">Rotation to apply</param>
    /// <returns>Offset in world or local space</returns>
    public Vector3 Offset(Quaternion _rot) => lookAtOffset.GetOffset(_rot);
    #endregion
}
