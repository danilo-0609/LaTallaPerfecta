namespace LaTallaPerfecta.BuildingBlocks.Domain;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
{
    public new AggregateRootId<TIdType> Id { get; protected set; }

    protected AggregateRoot(TId id) 
        : base(id)
    {
        Id = id;
    }
}
