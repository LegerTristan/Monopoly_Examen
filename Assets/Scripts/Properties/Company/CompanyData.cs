using UnityEngine;

/// <summary>
/// Data contained in company. Inherits from BuyData
/// </summary>
[CreateAssetMenu(fileName = "CompanyData", menuName = "ScriptableAssets/Monopoly/Buy/Company")]
public class CompanyData : PropertyData
{
    /// <summary>
    /// Scalars to apply on rent, there is values for if owner has all companies or not
    /// </summary>
    [SerializeField, Range(1, 100)]
    int priceScalar = 4, allCompagniesPriceScalar = 10;

    public int PriceScalar => priceScalar;
    public int AllCompagniesPriceScalar => allCompagniesPriceScalar;

}
