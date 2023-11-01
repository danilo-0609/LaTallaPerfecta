using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Products.DomainErrors;
using LaTallaPerfecta.Catalog.Domain.Products.Rules;
using MediatR;

namespace LaTallaPerfecta.Catalog.Domain.Products.ValueObjects;

public sealed record ProductName : ValueObject
{
    public string Value { get; private set; }
    
    public static ErrorOr<ProductName> Create(string name)
    {
        var checkRules = CheckRules(name);

        if (checkRules.IsError)
        {
            return checkRules.FirstError;
        }

        return new ProductName(name);
    }

    private static ErrorOr<Unit> CheckRules(string name)
    {
        NameLengthCannotBeMoreThan250LettersRule nameLengthCantBeOver250LettersRule = new(name);

        if (nameLengthCantBeOver250LettersRule.IsBroken())
        {
            return ProductErrors.Name.NameLengthOver250Letters(nameLengthCantBeOver250LettersRule.Message);
        }

        NameLengthCannotBeLessThan10Letters nameLengthCannotBeLessThan10LettersRule = new(name);

        if (nameLengthCannotBeLessThan10LettersRule.IsBroken())
        {
            return ProductErrors.Name.NameLengthLessThan10WordsLetters(nameLengthCannotBeLessThan10LettersRule.Message);
        }

        return Unit.Value;
    }

    private ProductName(string value)
    {
        Value = value;
    }
}
