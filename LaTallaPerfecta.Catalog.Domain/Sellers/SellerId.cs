using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Sellers;

public record SellerId : AggregateRootId<Ulid>
{
    private SellerId(Ulid value)
    {
        Value = value;
    }

    public override Ulid Value { get; protected set; }

    public static SellerId Create(Ulid value) => new SellerId(value);

}
