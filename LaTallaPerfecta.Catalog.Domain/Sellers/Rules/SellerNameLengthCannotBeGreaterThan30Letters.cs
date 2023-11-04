using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Sellers.Rules;

public sealed class SellerNameLengthCannotBeGreaterThan30Letters : IBusinessRule
{
    private readonly string _name;
    private const int MaximumLength = 30;

    public SellerNameLengthCannotBeGreaterThan30Letters(string name)
    {
        _name = name;
    }

    public string Message => "Seller name length cannot be greater than 100 letters";

    public bool IsBroken()
    {
        if (_name.Length > MaximumLength)
        {
            return true;
        }

        return false;
    }
}
