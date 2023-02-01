using UnityEngine;

/// <summary>
/// Inherits from Property, has a rent based on number of station the owner possessed.
/// </summary>
public class Station : Property
{
    #region F/P
    StationData data = null;

    public Material Visual => data ? data.BuyVisual : null;

    public string Name => data ? data.BuyName : "Unknown";

    public int PropertyCost => data ? data.BuyCost : 0;

    /// <summary>
    /// Returns base rent multiplied by number of station the owner have.
    /// </summary>
    public int Rent
    {
        get
        {
            if (!data)
                return 0;

            int _totalRent = data.Rent, 
                _occurences = owner.PropertyOccurences(IsPropertyTypeOfStation) - 1;

            for (int i = 0; i < _occurences; ++i)
                _totalRent *= 2;

            return _totalRent;
        }
    }
    #endregion

    #region CustomMethods
    public void Init(StationData _data)
    {
        data = _data;
    }
    public bool IsPropertyTypeOfStation(Property _property)
    {
        return _property is Station;
    }
    #endregion
}
