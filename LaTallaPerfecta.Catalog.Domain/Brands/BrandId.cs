using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Brands;

public sealed record BrandId : AggregateRootId<Ulid>
{
    public override Ulid Value { get; protected set; }

    public static BrandId Create(Ulid value) => new BrandId(value);

    public static BrandId CreateUnique() => new BrandId(Ulid.NewUlid());

    private BrandId(Ulid value)
        : base(value)
    {
        Value = value;
    }
}
