using System;
using UnityEngine;
//using UnityToolbox.GizmosTools;

/// <summary>
/// A RentCell that contains a plot.
/// </summary>
public class PlotCell : RentCell
{
    #region F/P
    const float DEFAULT_RIGHT_OFFSET = -0.017f;
    const float DEFAULT_FORWARD_OFFSET = 0.015f;

    //public event Action<Property> OnPropertyBought = null;
    public event Action<int> OnHousesBought = null;

    [SerializeField, Header("Property Cell")]
    PlotData plotData = null;

    [SerializeField]
    Offset houseSpawnOffset = new Offset
                            (
                                new Vector3(DEFAULT_RIGHT_OFFSET, 0f, DEFAULT_FORWARD_OFFSET),
                                true
                            );

    Plot plot = null;

    [SerializeField, Header("Debug")]
    Color debugColor = Color.magenta;

    [SerializeField, Range(0.01f, 1f)]
    float debugSize = 0.01f;

    [SerializeField]
    bool useDebug = true;

    public Vector3 HouseSpawnPoint => transform.position + houseSpawnOffset.GetOffset(transform);

    public int NbrHouseOn => plot != null ? plot.NbrHouse : 0;

    protected override string CellName => plot != null ? plot.Name : "Case propriété";

    protected override int Cost => plot != null ? plot.PropertyCost : 0;

    protected override int Rent => plot != null ? plot.Rent : 0;

    protected override bool IsRentCellValid => plotData;

    protected override MonopolyCharacter PropertyOwner => plot != null ? plot.Owner : null;
    #endregion

    #region CustomMethods
    protected override void InitRentCell()
    {
        base.InitRentCell();

        plot = new Plot();
        plot.Init(plotData);
        plot.OnPlotReset += ResetPlotCell;
        MonopolyGameManager.Instance?.HouseManager?.AddPlotCell(this);
    }

    private void OnDrawGizmos()
    {
        if (!useDebug)
            return;
        Gizmos.color = debugColor;
        Gizmos.DrawWireSphere(HouseSpawnPoint, debugSize);
    }

    protected override void PlayRentCellEffect(MonopolyCharacter _instigator)
    {
        if (!IsRentCellValid || plot == null)
            return;

        tempInstigator = _instigator;

        if (plot.Owner)
        {
            if (plot.Owner != tempInstigator)
                ApplyRent();
            else if (!plot.HasReachedMaxNbrHouses(plot.NbrHouse))
            {
                int _totalCost = 0, _nbrHouseBought = 0;
                ProposeToBuyHouses(_instigator, ref _totalCost, ref _nbrHouseBought);

                _instigator.Money.Current -= _totalCost;
                plot.Owner = tempInstigator;

                PrintCellEffect($"{tempInstigator} achète {_nbrHouseBought} maison.s" +
                    $" pour {_totalCost}{MonopolyGameManager.Instance?.Currency}", tempInstigator.Color);
                EndCellAction();
            }
        }
        else
            ProposeToBuy();
    }

    protected override void ProposeToBuy()
    {
        base.ProposeToBuy();
        tempInstigator.ChooseIfBuy(plotData);
    }

    protected override void GiveProperty()
    {
        int _totalCost = plot.PropertyCost, _nbrHouseBought = 0;
        ProposeToBuyHouses(tempInstigator, ref _totalCost, ref _nbrHouseBought);

        tempInstigator.Money.Current -= _totalCost;
        plot.Owner = tempInstigator;
        tempInstigator.AddProperty(plot);

        PrintCellEffect($"{tempInstigator} achète {plot.Name} et {_nbrHouseBought} maison.s" +
            $" pour {_totalCost}{MonopolyGameManager.Instance?.Currency}", tempInstigator.Color);
    }

    void ProposeToBuyHouses(MonopolyCharacter _instigator, ref int _previsionCost, 
        ref int _nbrBought)
    {
        while(_instigator.BuyHouse(_previsionCost) &&
            !plot.HasReachedMaxNbrHouses(plot.NbrHouse + _nbrBought))
        {
            _nbrBought++;
            _previsionCost += plot.HouseCost;
        }

        plot.NbrHouse += _nbrBought;

        if (_nbrBought > 0)
            OnHousesBought?.Invoke(_nbrBought);
    }

    void ResetPlotCell() => MonopolyGameManager.Instance?.HouseManager?.ClearHouses(this);
    #endregion
}
