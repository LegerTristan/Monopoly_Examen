using System;
using UnityEngine;

/// <summary>
/// Contains an offset, can be local or world offset.
/// </summary>
[Serializable]
public struct Offset
{
    #region F/P

    /// <summary>
    /// Position offset
    /// </summary>
    [SerializeField]
    Vector3 posOffset;

    /// <summary>
    /// Use local offset or world offset
    /// </summary>
    [SerializeField]
    bool useLocal;

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor for offset
    /// </summary>
    /// <param name="_posOffset">Position offset, default is new <see cref="Vector3"/> constructor</param>
    /// <param name="_useLocal">Boolean for using local space or world space</param>
    public Offset(Vector3 _posOffset = new Vector3(), bool _useLocal = false)
    {
        posOffset = _posOffset;
        useLocal = _useLocal;
    }
    #endregion

    #region Methods

    /// <summary>
    /// Returns current offset in local or world, depending on useLocal value.
    /// </summary>
    /// <param name="_origin">Origin of the offset, needed for local offset.</param>
    /// <returns>Returns the offset</returns>
    public Vector3 GetOffset(Transform _origin)
    {
        if (useLocal)
            return _origin.right * posOffset.x +
                    _origin.up * posOffset.y +
                    _origin.forward * posOffset.z;


        return posOffset;
    }

    /// <summary>
    /// Returns current offset in local or world, depending on useLocal value.
    /// </summary>
    /// <param name="_rot">Rotation which offset is based on, needed for local offset.</param>
    /// <returns>Returns the offset</returns>
    public Vector3 GetOffset(Quaternion _rot)
    {
        if (useLocal)
        {
            Vector3 _right = _rot * Vector3.right,
                    _up = _rot * Vector3.up,
                    _forward = _rot * Vector3.forward;

            return  _right * posOffset.x +
                    _up * posOffset.y +
                    _forward * posOffset.z;
        }

        return posOffset;
    }

    #endregion
}
