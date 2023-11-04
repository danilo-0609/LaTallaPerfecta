using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Sellers.Rules;

public sealed class SellerNameCannotBeEmpty : IBusinessRule
{
    private readonly string _name;

    public SellerNameCannotBeEmpty(string name)
    {
        _name = name;
    }

    public string Message => "Seller name cannot be empty";

    public bool IsBroken()
    {
        if (_name.Length == 0)
        {
            return true;
        }

        return false;
    }
}
