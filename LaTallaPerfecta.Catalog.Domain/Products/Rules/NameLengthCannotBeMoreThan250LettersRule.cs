using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Products.Rules;

internal sealed class NameLengthCannotBeMoreThan250LettersRule : IBusinessRule
{
    private const int MaxNameLength = 250;
    private readonly string _name;

    internal NameLengthCannotBeMoreThan250LettersRule(string name)
    {
        _name = name;
    }

    public string Message => "Name length cannot be more than 250 letters";

    public bool IsBroken()
    {
        if (_name.Length > MaxNameLength)
        {
            return true;
        }

        return false;
    }
}
