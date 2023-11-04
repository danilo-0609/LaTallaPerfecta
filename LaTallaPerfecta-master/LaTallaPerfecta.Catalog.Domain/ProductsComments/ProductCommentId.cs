using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.ProductsComments;

public sealed record ProductCommentId : AggregateRootId<Ulid>
{
    public override Ulid Value { get; protected set; }

    private ProductCommentId(Ulid value)
        : base(value)
    {
        Value = value;
    }

    public static ProductCommentId Create(Ulid value) => new ProductCommentId(value);

    public static ProductCommentId CreateUnique() => new ProductCommentId(Ulid.NewUlid());
}
