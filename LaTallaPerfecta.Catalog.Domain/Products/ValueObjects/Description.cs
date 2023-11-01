using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Products.DomainErrors;
using LaTallaPerfecta.Catalog.Domain.Products.Rules;
using MediatR;

namespace LaTallaPerfecta.Catalog.Domain.Products.ValueObjects;

public sealed record Description : ValueObject
{
    private readonly string _description;

    public string Value { get; private set; } = string.Empty;

    public static ErrorOr<Description> Create(string description)
    {
        var checkRules = CheckRules(description);

        if (checkRules.IsError)
        {
            return checkRules.FirstError;
        }

        return new Description(description);
    }

    private static ErrorOr<Unit> CheckRules(string description)
    {
        DescriptionLengthCannotBeGreaterThan5000Letters descriptionLengthOver5000LettersRule = new(description);
    
        if (descriptionLengthOver5000LettersRule.IsBroken())
        {
            return ProductErrors
                .Description
                .DescriptionLengthOver5000Letters(descriptionLengthOver5000LettersRule.Message);
        }

        return Unit.Value;
    }

    private Description(string description)
    {
        Value = description;
    }
