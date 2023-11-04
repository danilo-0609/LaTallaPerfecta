using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Brands.Events;
public sealed record BrandCreatedEvent(BrandId BrandId) : IDomainEvent;
