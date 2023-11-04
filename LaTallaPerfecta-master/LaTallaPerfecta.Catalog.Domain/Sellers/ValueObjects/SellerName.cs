using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Sellers.Errors;
using LaTallaPerfecta.Catalog.Domain.Sellers.Rules;
using MediatR;

namespace LaTallaPerfecta.Catalog.Domain.Sellers.ValueObjects;

public sealed record SellerName : ValueObject
{
    public string Value { get; private set; }

    public static ErrorOr<SellerName> Create(string sellerName)
    {
        var checkRules = CheckRules(sellerName);
        
        if (checkRules.IsError)
        {
            return checkRules.FirstError;
        }

        return new SellerName(sellerName);
    }

    private static ErrorOr<Unit> CheckRules(string sellerName)
    {
        SellerNameCannotBeEmpty cannotBeEmptyRule = new(sellerName);

        if (cannotBeEmptyRule.IsBroken())
        {
            return SellerErrors.Name.NameEmpty(cannotBeEmptyRule.Message);
        }

        SellerNameLengthCannotBeGreaterThan30Letters cannotBeGreaterThan30LettersRule = new(sellerName);
        
        if (cannotBeGreaterThan30LettersRule.IsBroken())
        {
            return SellerErrors.Name.NameLengthVeryExtensive(cannotBeGreaterThan30LettersRule.Message);
        }

        return Unit.Value;
    }

    public SellerName(string value)
    {
        Value = value;
    }
}
