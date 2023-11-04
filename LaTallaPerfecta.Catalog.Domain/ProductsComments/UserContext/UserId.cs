using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.ProductsComments.UserContext;

public sealed record UserId : EntityId<Ulid>
{
    public override Ulid Value { get; protected set; }

    public static UserId Create(Ulid value) => new UserId(value);

    public UserId(Ulid value)
        : base(value)
    {
        Value = value;
    }
}
