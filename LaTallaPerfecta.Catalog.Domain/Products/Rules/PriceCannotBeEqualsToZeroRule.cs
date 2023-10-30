using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Products.Rules;

internal sealed class PriceCannotBeEqualsToZeroRule : IBusinessRule
{
    private readonly decimal _price;

    internal PriceCannotBeEqualsToZeroRule(decimal price)
    {
        _price = price;
    }

    public string Message => "Price cannot be equals to zero.";

    public bool IsBroken()
    {
        if (_price == 0)
        {
            return true;
        }

        return false;
    }
}
