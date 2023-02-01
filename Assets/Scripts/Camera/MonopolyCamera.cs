using System;
using UnityEngine;
//using UnityToolbox.GizmosTools;

/// <summary>
/// Camera used for monopoly game.
/// Requires a camera and a lerp move component in order to work.
/// </summary>
[RequireComponent(typeof(Camera), typeof(FollowComponent))]
public class MonopolyCamera : MonoBehaviour
{
    #region F/P

    const float CENTER = 0.5f;

    /// <summary>
    /// Invoked every LateUpdate.
    /// </summary>
    public event Action OnUpdateCamera = null;

    /// <summary>
    ///  Target to follow and look at
    /// </summary>
    [SerializeField]
    Transform target = null;

    /// <summary>
    /// Move component that applies follow
    /// </summary>
    [SerializeField, Header("Move")]
    FollowComponent followComp = null;

    Camera cam = null;

    /// <summary>
    /// Show debug or not
    /// </summary>
    [SerializeField, Header("Debug")]
    bool useDebug = true;

    /// <summary>
    /// Debug color used for identifying target
    /// </summary>
    [SerializeField]
    Color targetColor = Color.green;

    /// <summary>
    /// Size used for cube and sphere debug
    /// </summary>
    [SerializeField, Range(0.01f, 1f)]
    float cubeSize = 0.1f, debugSize = 0.1f;

    public Vector3 GetCenterViewport(float _depth)
    {
        return cam ? cam.ViewportToWorldPoint(new Vector3(CENTER, CENTER, _depth)) : Vector3.zero;
    }

    public Vector3 CurrentPosition => transform.position;

    Vector3 TargetPosition => target ? target.position : Vector3.zero;

    Vector3 LookAtPosition => TargetPosition + followComp.LookAtPosition(target);

    Vector3 FollowPosition => TargetPosition + followComp.FollowPosition(target);

    bool IsCameraValid => target != null && followComp != null
    && followComp.IsLerpMoveComponentValid;

    #endregion

    #region UnityEvents
    void Start() => Init();

    void LateUpdate() => OnUpdateCamera?.Invoke();

    private void OnDrawGizmos()
    {
        if (!useDebug)
            return;

        if(!IsCameraValid)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(CurrentPosition, new Vector3(cubeSize, cubeSize, cubeSize));
            return;
        }

        Gizmos.color = targetColor;
        Gizmos.DrawLine(CurrentPosition, TargetPosition);
        Gizmos.DrawWireSphere(CurrentPosition, debugSize);
        Gizmos.DrawWireSphere(TargetPosition, debugSize);

        Gizmos.color = followComp.LookAtSettings.DebugColor;
        Gizmos.DrawLine(CurrentPosition, LookAtPosition);
        Gizmos.DrawWireCube(LookAtPosition, new Vector3(debugSize, debugSize, debugSize));

        Gizmos.color = followComp.FollowSettings.DebugColor;
        Gizmos.DrawLine(CurrentPosition, FollowPosition);
        Gizmos.DrawWireCube(FollowPosition, new Vector3(debugSize, debugSize, debugSize));
    }
    #endregion

    #region CustomMethods

    /// <summary>
    /// Setter for target
    /// </summary>
    /// <param name="_target">New target to follow and look at</param>
    public void SetTarget(Transform _target) => target = _target;

    void Init()
    {
        cam = GetComponent<Camera>();

        OnUpdateCamera += () =>
        {
            if (!target)
                return;

            followComp.LookAtTarget(target);
            followComp.MoveToTarget(target);
        };
    }
    #endregion
}
