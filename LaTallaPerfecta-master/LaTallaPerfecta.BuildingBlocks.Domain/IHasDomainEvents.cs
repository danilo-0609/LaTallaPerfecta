namespace LaTallaPerfecta.BuildingBlocks.Domain;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }

    public void ClearDomainEvent();
}
