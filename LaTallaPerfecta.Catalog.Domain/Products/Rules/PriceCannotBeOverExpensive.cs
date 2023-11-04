using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Products.Rules;

internal class PriceCannotBeOverExpensive : IBusinessRule
{
    private const decimal MaxAmountOfPrice = 100000000;
    private readonly decimal _price;

    internal PriceCannotBeOverExpensive(decimal price)
    {
        _price = price;
    }

    public string Message => "Price cannot be more expensive than 100.000.000 COP";

    public bool IsBroken()
    {
        if (_price > MaxAmountOfPrice)
        {
            return true;
        }

        return false;
    }
}
