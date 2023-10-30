using LaTallaPerfecta.BuildingBlocks.Domain;
using MediatR;

namespace LaTallaPerfecta.BuildingBlocks.Application.Events;

public interface IDomainEventHandler<TDomainEventNotification> : INotificationHandler<TDomainEventNotification>
    where TDomainEventNotification : IDomainEvent
{
}
