using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObject that display a card mesh with a specific visual in front of the camera.
/// </summary>
public class CardVisual : SingletonTemplate<CardVisual>
{
    MonopolyCamera cam = null;

    /// <summary>
    /// GameObject to used as CardVisual
    /// </summary>
    [SerializeField]
    GameObject visual = null;

    MeshRenderer meshRenderer = null;

    /// <summary>
    /// Min depth and max depth from the camera.
    /// </summary>
    [SerializeField, Range(0.01f, 1f)]
    float minDepth = 0.1f, maxDepth = 0.2f;

    /// <summary>
    /// Scalar applied to <see cref="currentDepth"/> translation to max from min
    /// in order to slow it.
    /// </summary>
    [SerializeField, Range(1f, 100f)]
    float slow = 3f;

    float currentDepth = 0f;

    bool IsCardVisualValid => visual && cam && meshRenderer;

    private void Start() => InitCardVisual();

    private void LateUpdate() => PlaceTowardsCamera();

    void InitCardVisual()
    {
        cam = MonopolyGameManager.Instance?.MainCam;

        if(visual)
            meshRenderer = visual.GetComponent<MeshRenderer>();

        if (IsCardVisualValid)
            visual.SetActive(false);
    }

    /// <summary>
    /// Display cardVisual in the scene with a specific material.
    /// reset currentDepth to max
    /// </summary>
    /// <param name="_toRender">Material to render</param>
    public void PrintCardVisual(Material _toRender)
    {
        if (!IsCardVisualValid)
            return;

        visual.SetActive(true);
        meshRenderer.material = _toRender;
        currentDepth = maxDepth;
    }

    /// <summary>
    /// Hide card visual in the scene
    /// </summary>
    public void HideCardVisual()
    {
        if (!IsCardVisualValid)
            return;

        visual.SetActive(false);
    }

    /// <summary>
    /// Set position and rotation based on camera viewpoint.
    /// Bring visual closer if possible.
    /// </summary>
    void PlaceTowardsCamera()
    {
        if (!IsCardVisualValid)
            return;

        if(visual.activeInHierarchy)
            BringCloser();

        transform.position = cam.GetCenterViewport(currentDepth);
        Vector3 _fwd = (cam.CurrentPosition - transform.position).normalized;

        if (_fwd != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(_fwd);
    }

    void BringCloser()
    {
        currentDepth -= Time.deltaTime / slow;
        currentDepth = currentDepth < minDepth ? minDepth : currentDepth;
    }
}
