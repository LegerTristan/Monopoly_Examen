using System;
using UnityEngine;

/// <summary>
/// Component for lerping from a position A to position B
/// </summary>
public class FollowComponent : MonoBehaviour
{
    #region F/P

    /// <summary>
    /// Invoked when owner reached the follow position.
    /// </summary>
    public event Action OnMoveEnded = null;

    /// <summary>
    /// Settings used for the lerping
    /// </summary>
    [SerializeField]
    FollowSettings followSettings = null;

    [SerializeField]
    LookAtSettings lookAtSettings = null;

    Vector3 CurrentPosition => transform.position;

    Quaternion CurrentRotation => transform.rotation;

    /// <summary>
    /// Getter for follow settings
    /// </summary>
    public FollowSettings FollowSettings=> followSettings;

    /// <summary>
    /// Getter for look at settings
    /// </summary>
    public LookAtSettings LookAtSettings => lookAtSettings;

    /// <summary>
    /// Check if component is valid (settings != null)
    /// </summary>
    public bool IsLerpMoveComponentValid => followSettings != null;
    #endregion

    #region Methods
    /// <summary>
    /// Returns follow positions based on settings and target
    /// </summary>
    /// <param name="_target">Target to follow</param>
    /// <returns>Return a position in world or local space depending on settings</returns>
    public Vector3 FollowPosition(Transform _target) => _target.position + followSettings.Offset(_target);

    /// <summary>
    /// Returns look at positions based on settings and target
    /// </summary>
    /// <param name="_target">Target to look at</param>
    /// <returns>Return a position in world or local space depending on settings</returns>
    public Vector3 LookAtPosition(Transform _target) => _target.position + lookAtSettings.Offset(_target);


    /// <summary>
    /// Apply a lerp movement from current position to target position with settings
    /// stocked in the component
    /// </summary>
    /// <param name="_target">Target to follow</param>
    public void MoveToTarget(Transform _target)
    {
        if (!_target || !IsLerpMoveComponentValid)
            return;

        Vector3 _followPos = FollowPosition(_target);

        transform.position = followSettings.UseSmoothMove ?
                    Vector3.Lerp(CurrentPosition, _followPos, Time.deltaTime * followSettings.MoveSpeed) :
                    Vector3.MoveTowards(CurrentPosition, _followPos, Time.deltaTime * followSettings.MoveSpeed);

        if (CurrentPosition == _followPos)
            OnMoveEnded?.Invoke();
    }

    /// <summary>
    /// Look at the followed target with settings stocked in the look at settings.
    /// </summary>
    /// <param name="_target">Target to look at</param>
    public void LookAtTarget(Transform _target)
    {
        if (!lookAtSettings.UseLookAt || !_target)
            return;

        if (lookAtSettings.StopLookAtWhenMoveStop && FollowPosition(_target) == CurrentPosition)
            return;

        Quaternion _rot = Quaternion.LookRotation((LookAtPosition(_target) - CurrentPosition).normalized);

        transform.rotation = lookAtSettings.UseSmoothLookAt ?
                    Quaternion.Lerp(CurrentRotation, _rot, Time.deltaTime * lookAtSettings.LookAtSpeed) :
                    Quaternion.RotateTowards(CurrentRotation, _rot, Time.deltaTime * lookAtSettings.LookAtSpeed);
    }

    #endregion
}
