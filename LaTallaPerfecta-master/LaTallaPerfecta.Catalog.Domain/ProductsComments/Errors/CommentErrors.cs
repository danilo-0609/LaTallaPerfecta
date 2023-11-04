using ErrorOr;

namespace LaTallaPerfecta.Catalog.Domain.ProductsComments.Errors;

public static class CommentErrors
{
    public static class Comment
    {
        public static Error CommentGreaterThanAllowed(string message) =>
            Error.Validation("Comment.GreaterThanAllowed", message);
    }
}
