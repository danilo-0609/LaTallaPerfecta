using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Products.DomainErrors;
using LaTallaPerfecta.Catalog.Domain.Products.Rules;
using MediatR;

namespace LaTallaPerfecta.Catalog.Domain.Products.ValueObjects;

public sealed record Price : ValueObject
{
    public decimal Value { get; private set; } = decimal.Zero;

    public static ErrorOr<Price> Create(decimal price)
    {
        var checkRules = CheckRules(price);

        if (checkRules.IsError)
        {
            return checkRules.FirstError;
        }

        return new Price(price);
    }

    private static ErrorOr<Unit> CheckRules(decimal price)
    {
        PriceCannotBeEqualsToZeroRule priceEqualsToZeroRule = new(price);

        if (priceEqualsToZeroRule.IsBroken())
        {
            return Errors.Price.PriceEqualsToZero(priceEqualsToZeroRule.Message);
        }

        PriceCannotBeOverExpensive priceOverExpensiveRule = new(price);

        if (priceOverExpensiveRule.IsBroken())
        {
            return Errors.Price.PriceOverExpensive(priceOverExpensiveRule.Message);
        }

        return Unit.Value;
    } 

    private Price(decimal value)
    {
        Value = value;
    }
}
