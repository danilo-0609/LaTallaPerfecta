using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.ProductsComments.Events;

public record CommentCreatedEvent(ProductCommentId ProductCommentId) : IDomainEvent;
