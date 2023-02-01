using UnityEngine;

/// <summary>
/// Data contained in a plot
/// </summary>
[CreateAssetMenu(fileName = "PlotData", menuName = "ScriptableAssets/Monopoly/Buy/Plot")]
public sealed class PlotData : PropertyData
{
    /// <summary>
    /// Cost for buying a new house or hotel
    /// </summary>
    [SerializeField, Range(1, 20000)]
    int houseCost = 50;

    /// <summary>
    /// Array of rent based on number of houses the plot possessed
    /// </summary>
    [SerializeField, Range(1, 10000)]
    int[] rents = new int[Plot.MAX_NBR_HOUSES + 1];

    public int HouseCost => houseCost;
    public int[] Rents => rents;
}
