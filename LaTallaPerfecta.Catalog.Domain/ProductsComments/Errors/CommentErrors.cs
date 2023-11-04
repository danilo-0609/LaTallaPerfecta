using ErrorOr;

namespace LaTallaPerfecta.Catalog.Domain.ProductsComments.Errors;

public sealed class CommentErrors
{
    public class Comment
    {
        public static Error CommentGreatherThanAllowed(string message) =>
            Error.Validation("Comment.GreaterThanAllowed", message);
    }
}
