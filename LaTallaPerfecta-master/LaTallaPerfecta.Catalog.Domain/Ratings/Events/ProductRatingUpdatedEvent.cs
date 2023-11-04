using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Ratings.Events;

public record ProductRatingUpdatedEvent(ProductRatingId ProductRatingId) : IDomainEvent;

