using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Products.Rules;

internal sealed class NameLengthCannotBeLessThan10Letters : IBusinessRule
{
    private readonly string _name;

    internal NameLengthCannotBeLessThan10Letters(string name)
    {
        _name = name;
    }

    public string Message => "Name length cannot be less than 10 letters";

    public bool IsBroken()
    {
        if (_name.Length < 10)
        {
            return true;
        }

        return false;
    }
}
