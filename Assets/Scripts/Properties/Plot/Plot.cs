using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherits from Property. A plot have a rent and a number of houses
/// </summary>
public class Plot : Property
{
    #region F/P
    /// <summary>
    /// Max number of houses on every plot
    /// </summary>
    public const int MAX_NBR_HOUSES = 5;

    /// <summary>
    /// Invoke when a plot is reset through ResetProperty
    /// </summary>
    public Action OnPlotReset = null;

    PlotData data = null;

    int nbrHouse = 0;

    public Material Visual => data ? data.BuyVisual : null;

    public string Name => data ? data.BuyName : "Unknown";

    public int PropertyCost => data ? data.BuyCost : 0;

    public int HouseCost => data ? data.HouseCost : 0;

    /// <summary>
    /// Returns rent based on number of houses the owner have.
    /// </summary>
    public int Rent
    {
        get
        {
            if (!data || data.Rents == null || data.Rents.Length < nbrHouse)
                return 0;

            return data.Rents[nbrHouse];
        }
    }

    /// <summary>
    /// Returns number of houses, this differentiates houses from hotel
    /// </summary>
    public int NbrHouse 
    {
        get
        {
            return nbrHouse < MAX_NBR_HOUSES ? nbrHouse : 0;
        }
        set => nbrHouse = value;
    }

    /// <summary>
    /// Returns number of hotel, this differentiates houses from hotel
    /// </summary>
    public int NbrHotel => nbrHouse == MAX_NBR_HOUSES ? 1 : 0;

    #endregion

    #region CustomMethods
    public void Init(PlotData _data)
    {
        data = _data;
    }

    public bool HasReachedMaxNbrHouses(int _currentNbr) => _currentNbr >= MAX_NBR_HOUSES;

    /// <summary>
    /// Reset owner and number of houses, invoke OnPlotReset.
    /// </summary>
    public override void Reset()
    {
        base.Reset();

        nbrHouse = 0;
        OnPlotReset?.Invoke();
    }
    #endregion
}
