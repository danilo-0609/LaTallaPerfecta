using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.ProductsComments.Errors;
using LaTallaPerfecta.Catalog.Domain.ProductsComments.Rules;
using MediatR;

namespace LaTallaPerfecta.Catalog.Domain.ProductsComments.ValueObjects;

public sealed record Comment : ValueObject
{
    public string Value { get; private set; }

    public static ErrorOr<Comment> Create(string comment)
    {
        var checkRules = CheckRules(comment);

        if (checkRules.IsError)
        {
            return checkRules.FirstError;
        }

        return new Comment(comment);
    }

    private static ErrorOr<Unit> CheckRules(string comment)
    {
        CommentLengthCannotBeGreaterThan500Letters cannotBeGreaterThan500LettersRule = new(comment);

        if (cannotBeGreaterThan500LettersRule.IsBroken())
        {
            return CommentErrors.Comment.CommentGreatherThanAllowed(cannotBeGreaterThan500LettersRule.Message);
        }

        return Unit.Value;
    }

    private Comment(string value)
    {
        Value = value;
    }
}
