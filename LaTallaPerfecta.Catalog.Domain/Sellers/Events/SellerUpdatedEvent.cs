using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Sellers.Events;

public sealed record SellerUpdatedEvent(Seller seller) : IDomainEvent;

