using System;
using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Ratings.Rules;

public sealed class RatingCannotBeZeroOrGreaterThan5 : IBusinessRule
{
    private readonly double _value;

    public RatingCannotBeZeroOrGreaterThan5(double value)
    {
        _value = value;
    }

    public string Message => "Rating cannot be zero (0) or greater than 5.0";

    public bool IsBroken()
    {
        if (_value > 5.0 || _value < 1)
        {
            return true;
        }

        return false;
    }
}
