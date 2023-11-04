using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Brands.Events;

public record BrandUpdatedEvent(BrandId BrandId) : IDomainEvent;
