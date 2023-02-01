using UnityEngine;
using static System.Collections.Specialized.BitVector32;

/// <summary>
/// A RentCell that contains a station.
/// </summary>
public class StationCell : RentCell
{
    #region F/P
    [SerializeField]
    StationData stationData = null;

    Station station = null;

    protected override int Rent => station != null ? station.Rent : 0;

    protected override string CellName => stationData.BuyName;

    protected override int Cost => stationData ? stationData.BuyCost : 0;

    protected override bool IsRentCellValid => stationData;

    protected override MonopolyCharacter PropertyOwner => station != null ? station.Owner : null;
    #endregion

    #region CustomMethods
    protected override void InitRentCell()
    {
        base.InitRentCell();

        station = new Station();
        station.Init(stationData);
    }

    protected override void ProposeToBuy()
    {
        base.ProposeToBuy();
        tempInstigator.ChooseIfBuy(stationData);
    }

    protected override void PlayRentCellEffect(MonopolyCharacter _instigator)
    {
        if (!IsRentCellValid || station == null)
            return;

        tempInstigator = _instigator;

        if (station.Owner)
        {
            if (station.Owner != tempInstigator)
                ApplyRent();
        }
        else
            ProposeToBuy();
    }

    protected override void GiveProperty()
    {
        station.Owner = tempInstigator;
        tempInstigator.Money.Current -= stationData.BuyCost;
        tempInstigator.AddProperty(station);

        PrintCellEffect($"{tempInstigator} achète {stationData.BuyName} " +
            $" pour {stationData.BuyCost}{MonopolyGameManager.Instance?.Currency}", tempInstigator.Color);
    }
    #endregion
}
