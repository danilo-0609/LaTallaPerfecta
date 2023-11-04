using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Ratings.Errors;
using LaTallaPerfecta.Catalog.Domain.Ratings.Rules;
using MediatR;

namespace LaTallaPerfecta.Catalog.Domain.Ratings.ValueObjects;

public sealed record Rating : ValueObject
{
    public double Value { get; private set; }

    private Rating(double value) 
    {
        Value = value;
    }

    public static ErrorOr<Rating> Create(double value)
    {
        var checkRules = CheckRules(value);

        if (checkRules.IsError)
        {
            return checkRules.FirstError;
        }

        return new Rating(value);
    }

    private static ErrorOr<Unit> CheckRules(double value)
    {
        RatingCannotBeZeroOrGreaterThan5 cannotBeZeroOrGreaterThan5Rule = new(value);

        if (cannotBeZeroOrGreaterThan5Rule.IsBroken())
        {
            return RatingErrors.InvalidDouble(cannotBeZeroOrGreaterThan5Rule.Message);
        }

        return Unit.Value;
    }
}
