using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Products.ValueObjects;

public sealed record Rating : ValueObject
{
    public double Value { get; private set; }
    
    private Rating(double value)
    {
        Value = value;
    }

    public static Rating Create(double value)
    {
        return new Rating(value);
    }
}
