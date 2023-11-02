using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Products;
using LaTallaPerfecta.Catalog.Domain.ProductsComments.Events;
using LaTallaPerfecta.Catalog.Domain.ProductsComments.UserContext;
using LaTallaPerfecta.Catalog.Domain.ProductsComments.ValueObjects;

namespace LaTallaPerfecta.Catalog.Domain.ProductsComments;

public sealed class ProductComment : AggregateRoot<ProductCommentId, Ulid>
{
    public ProductCommentId ProductCommentId { get; }

    public UserId UserId { get; private set; }

    public ProductId ProductId { get; private set; }

    public Comment Comment { get; private set; } 

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }


    public static ErrorOr<ProductComment> Create(Ulid userIdValue,
        ProductId productId,
        string commentValue)
    {
        var productCommentId = ProductCommentId.CreateUnique();
        var userId = UserId.Create(userIdValue);

        var commentString = Comment.Create(commentValue);

        if (commentString.IsError)
        {
            return commentString.FirstError;
        }



        var comment = new ProductComment(productCommentId,
            userId,
            productId,
            commentString.Value,
            DateTime.UtcNow,
            null);

        comment.AddDomainEvent(new CommentCreatedEvent(productCommentId));

        return comment;
    }

    public static ErrorOr<ProductComment> Update(Ulid userIdValue,
        ProductId productId,
        string commentValue, DateTime createdDateTime)
    {
        var productCommentId = ProductCommentId.CreateUnique();
        var userId = UserId.Create(userIdValue);

        var commentString = Comment.Create(commentValue);

        if (commentString.IsError)
        {
            return commentString.FirstError;
        }

        var comment = new ProductComment(productCommentId,
            userId,
            productId,
            commentString.Value,
            createdDateTime,
            DateTime.UtcNow);

        comment.AddDomainEvent(new CommentUpdatedEvent(productCommentId));

        return comment;
    }



    private ProductComment(ProductCommentId id,
        UserId userId,
        ProductId productId,
        Comment comment, 
        DateTime createdDateTime,
        DateTime? updatedDateTime) : base(id)
    {
        ProductCommentId = id;
        UserId = userId;
        ProductId = productId;
        Comment = comment;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }
}
