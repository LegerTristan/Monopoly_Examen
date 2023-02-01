
/// <summary>
/// Inherits from Property. Rent is based on number of companies the owner owned
/// and on the dice results of the character that lands on.
/// </summary>
public class Company : Property
{
    /// <summary>
    /// Number of companies available inn the game.
    /// </summary>
    public static int NUMBER_COMPAGNIES = 0;

    CompanyData data;

    /// <summary>
    /// Set <see cref="CompanyData"/> and increase number of companies.
    /// </summary>
    /// <param name="_data">Data of the company</param>
    public void Init(CompanyData _data)
    {
        data = _data;
        NUMBER_COMPAGNIES++;
    }

    /// <summary>
    /// Returns rent based on it owner has all companies or not, and on 
    /// dice reults of the character that lands on.
    /// </summary>
    /// <param name="_character">CHaracter to apply rent on</param>
    /// <returns>Returns rent amount</returns>
    public int Rent(MonopolyCharacter _character)
    {
        return HasCharacterAllCompagnies(owner) ?
            data.AllCompagniesPriceScalar * _character.DiceResult :
            data.PriceScalar * _character.DiceResult;
    }

    /// <summary>
    /// Check if a character has all companies.
    /// </summary>
    /// <param name="_character">Character to check</param>
    /// <returns>Returns true if character has all companies, else false</returns>
    bool HasCharacterAllCompagnies(MonopolyCharacter _character)
    {
        return _character.PropertyOccurences(IsPropertyTypeOfCompany) == NUMBER_COMPAGNIES;
    }

    public bool IsPropertyTypeOfCompany(Property _property)
    {
        return _property is Company;
    }
}
