using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.ProductsComments.Rules;

public sealed class CommentLengthCannotBeGreaterThan500Letters : IBusinessRule
{
    private readonly string _comment;
    private const int MaximumLength = 500;
    
    public CommentLengthCannotBeGreaterThan500Letters(string comment)
    {
        _comment = comment;
    }

    public string Message => "Comment length cannot be greater than 500 letters";

    public bool IsBroken()
    {
        if (_comment.Length > MaximumLength)
        {
            return true;
        }

        return false;
    }
}
