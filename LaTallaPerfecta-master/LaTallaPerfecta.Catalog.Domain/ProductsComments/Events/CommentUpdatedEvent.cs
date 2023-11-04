using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Products;

namespace LaTallaPerfecta.Catalog.Domain.ProductsComments.Events;

public sealed record CommentUpdatedEvent(ProductCommentId ProductCommentId) : IDomainEvent;
