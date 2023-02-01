using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

/// <summary>
/// A RentCell that contains a company.
/// </summary>
public class CompanyCell : RentCell
{
    #region F/P
    [SerializeField]
    CompanyData companyData = null;

    Company company = null;

    protected override int Rent => tempInstigator && company != null ?
        company.Rent(tempInstigator) : 0;

    protected override string CellName => companyData.BuyName;

    protected override int Cost => companyData ? companyData.BuyCost : 0;

    protected override bool IsRentCellValid => companyData;

    protected override MonopolyCharacter PropertyOwner => company != null ? company.Owner : null;
    #endregion

    #region CustomMethods
    protected override void InitRentCell()
    {
        base.InitRentCell();

        company = new Company();
        company.Init(companyData);
    }

    protected override  void ProposeToBuy()
    {
        base.ProposeToBuy();
        tempInstigator.ChooseIfBuy(companyData);
    }

    protected override void PlayRentCellEffect(MonopolyCharacter _instigator)
    {
        if (!IsRentCellValid || company == null)
            return;

        tempInstigator = _instigator;

        if (company.Owner)
        {
            if (company.Owner != tempInstigator)
                ApplyRent();
        }
        else
            ProposeToBuy();
    }

    protected override void GiveProperty()
    {
        company.Owner = tempInstigator;
        tempInstigator.Money.Current -= companyData.BuyCost;
        tempInstigator.AddProperty(company);

        PrintCellEffect($"{tempInstigator} achète {companyData.BuyName} " +
            $" pour {companyData.BuyCost}{MonopolyGameManager.Instance?.Currency}", tempInstigator.Color);
    }
    #endregion
}
