using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage houses and hotels on the board
/// </summary>
public class MonopolyHouseManager : MonoBehaviour
{
    #region F/P
    /// <summary>
    /// Dictionary containing each plot cells with their houses on
    /// </summary>
    Dictionary<PlotCell, List<GameObject>> cellHouses = 
        new Dictionary<PlotCell, List<GameObject>>();

    [SerializeField, Header("HouseManager")]
    GameObject housePrefab = null;

    [SerializeField]
    GameObject hotelPrefab = null;

    /// <summary>
    /// Distannce between each houses
    /// </summary>
    [SerializeField, Range(0.001f, 1f)]
    float houseGap = 0.005f;

    bool IsHouseManagerValid => housePrefab && hotelPrefab;
    #endregion

    #region CustomMethods
    /// <summary>
    /// Add a new plot cell to <see cref="cellHouses"/>
    /// </summary>
    /// <param name="_propertyCell">Cell to add to the dictionary</param>
    public void AddPlotCell(PlotCell _propertyCell)
    {
        if (!IsHouseManagerValid || cellHouses.ContainsKey(_propertyCell))
            return;

        _propertyCell.OnHousesBought += (_nbrBought) => SpawnNewHouses(_propertyCell, _nbrBought);
        cellHouses.Add(_propertyCell, new List<GameObject>());
    }

    /// <summary>
    /// Spawn a specific number of houses on the cell passed in parameters.
    /// </summary>
    /// <param name="_cell">Cell to spawn new houses</param>
    /// <param name="_nbrToSpawn">Number of houses to spawn</param>
    void SpawnNewHouses(PlotCell _cell, int _nbrToSpawn)
    {
        if (!cellHouses.ContainsKey(_cell))
            return;

        List<GameObject> _spawnedHouses = cellHouses[_cell];

        if((_spawnedHouses.Count + _nbrToSpawn) >= Plot.MAX_NBR_HOUSES)
        {
            ClearHouses(_spawnedHouses);
            _spawnedHouses.Add(CreateHotel(_cell, 1));
        }
        else
        {
            for (int i = 0; i < _nbrToSpawn; ++i)
                _spawnedHouses.Add(CreateHouse(_cell, i + _spawnedHouses.Count));
        }
    }
    
    /// <summary>
    /// Destroy all houses on a cell and remove them from the list
    /// </summary>
    /// <param name="_cell">Cell to destroy houses</param>
    public void ClearHouses(PlotCell _cell)
    {
        if (cellHouses.ContainsKey(_cell))
            ClearHouses(cellHouses[_cell]);
    }

    /// <summary>
    /// Destroy all houses from the list passed in parameters
    /// </summary>
    /// <param name="_houses">List of houses to destroy</param>
    void ClearHouses(List<GameObject> _houses)
    {
        for (int i = 0; i < _houses.Count; ++i)
            if (_houses[i] != null)
                Destroy(_houses[i].gameObject);

        _houses.Clear();
    }

    /// <summary>
    /// Create a new house with parent, name and position depending on parameters
    /// </summary>
    /// <param name="_parent">Parent of the house</param>
    /// <param name="_scalar">Index of the house, used as a scalar for name and position</param>
    /// <returns>A new House</returns>
    GameObject CreateHouse(PlotCell _parent, int _scalar)
    {
        GameObject _house = Instantiate(housePrefab);
        _house.name = $"{_parent}_House_{_scalar}";
        _house.transform.position = GetSpawnPosition(_parent, _scalar);
        _house.transform.SetParent(_parent.transform);

        return _house;
    }


    /// <summary>
    /// Create a new hotel with parent, name and position depending on parameters
    /// </summary>
    /// <param name="_parent">Parent of the hotel</param>
    /// <param name="_scalar">Index of the hotel, used as a scalar for name and position</param>
    /// <returns>A new hotel</returns>
    GameObject CreateHotel(PlotCell _parent, int _scalar)
    {
        GameObject _house = Instantiate(hotelPrefab);
        _house.name = $"{_parent}_Hotel";
        _house.transform.position = GetSpawnPosition(_parent, _scalar);
        _house.transform.SetParent(_parent.transform);

        return _house;
    }

    /// <summary>
    /// Get a position based on an origin and a scalar
    /// </summary>
    /// <param name="_origin">Origin of the spawn</param>
    /// <param name="_scalar">Index used as a scalar to aply an offset on local right axis</param>
    /// <returns></returns>
    Vector3 GetSpawnPosition(PlotCell _origin, int _scalar)
    {
        return _origin.HouseSpawnPoint + (_origin.transform.right * (houseGap * _scalar));
    }
    #endregion
}
