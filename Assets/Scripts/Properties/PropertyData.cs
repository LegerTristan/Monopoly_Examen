using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data contained in a property.
/// </summary>
public class PropertyData : ScriptableObject
{
    /// <summary>
    /// Material of the property
    /// </summary>
    [SerializeField]
    protected Material buyVisual = null;

    /// <summary>
    /// Name of the property
    /// </summary>
    [SerializeField]
    protected string buyName = "Boulevard de Belleville";

    /// <summary>
    /// Cost of the property
    /// </summary>
    [SerializeField, Range(1, 30000)]
    protected int buyCost = 60;

    public Material BuyVisual => buyVisual;
    public string BuyName => buyName;
    public int BuyCost => buyCost;
}
