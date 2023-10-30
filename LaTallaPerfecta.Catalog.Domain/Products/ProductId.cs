using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Products;

public record ProductId : AggregateRootId<Ulid>
{
    public override Ulid Value { get; protected set; }

    private ProductId(Ulid value)
        : base(value)
    {
        Value = value;
    }

    public static ProductId CreateUnique() => new ProductId(Ulid.NewUlid());

    public static ProductId Create(Ulid id)
    {
        return new ProductId(id);
    }
}
