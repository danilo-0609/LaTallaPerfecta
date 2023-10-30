using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Products.Rules;

internal sealed class DescriptionLengthCannotBeGreaterThan5000Letters : IBusinessRule
{
    private readonly string _description;
    private const int MaximumDescriptionLength = 5000;

    internal DescriptionLengthCannotBeGreaterThan5000Letters(string description)
    {
        _description = description;
    }

    public string Message => "Description length cannot be greater than 500 letters";

    public bool IsBroken()
    {
        if (_description.Length > MaximumDescriptionLength)
        {
            return true;
        }

        return false;
    }
}
