using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Ratings.Events;

public record ProductRatingCreatedEvent(ProductRatingId ProductRatingId) : IDomainEvent;

