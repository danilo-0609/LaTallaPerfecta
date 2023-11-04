using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Ratings;

public sealed record ProductRatingId : AggregateRootId<Ulid>
{
    public override Ulid Value { get; protected set; }

    private ProductRatingId(Ulid value)
    {
        Value = value;
    }

    public static ProductRatingId Create(Ulid value) => new ProductRatingId(value);

    public static ProductRatingId CreateUnique() => new ProductRatingId(Ulid.NewUlid());

}


