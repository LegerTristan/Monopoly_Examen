using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data contained in a station
/// </summary>
[CreateAssetMenu(fileName = "StationData", menuName = "ScriptableAssets/Monopoly/Buy/Station")]
public class StationData : PropertyData
{
    /// <summary>
    /// Base rent of the station
    /// </summary>
    [SerializeField]
    int rent = 2;

    /// <summary>
    /// Scalar applied to rent depending on number of stations the owner have.
    /// </summary>
    [SerializeField]
    int occurenceScalar = 2;

    public int Rent => rent;

    public int OccurenceScalar => occurenceScalar;
}
