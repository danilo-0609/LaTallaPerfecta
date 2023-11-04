using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Common.UserContext;
using LaTallaPerfecta.Catalog.Domain.Ratings.Events;
using LaTallaPerfecta.Catalog.Domain.Ratings.ValueObjects;

namespace LaTallaPerfecta.Catalog.Domain.Ratings;
public sealed class ProductRating : AggregateRoot<ProductRatingId, Ulid>
{
    public ProductRatingId ProductRatingId { get; private set; }

    public string? RatingComment { get; private set; }

    public UserId UserId { get; private set; }

    public Rating Rating { get; private set; }


    private ProductRating(ProductRatingId id,
        UserId userId,
        Rating rating,
        string? ratingComment = null) 
        : base(id)
    {
        ProductRatingId = id;
        RatingComment = ratingComment;
        UserId = userId;
        Rating = rating;
    }

    public static ErrorOr<ProductRating> Create(double ratingValue,
        UserId userId,
        string? ratingComment = null)
    {
        var productRatingId = ProductRatingId.CreateUnique();
        var rating = Rating.Create(ratingValue);

        if (rating.IsError)
        {
            return rating.FirstError;
        }

        var productRating = new ProductRating(productRatingId,
            userId,
            rating.Value,
            ratingComment);
    
        productRating.AddDomainEvent(new ProductRatingCreatedEvent(productRatingId));

        return productRating;
    }

    public static ErrorOr<ProductRating> Update(Ulid id,
        double ratingValue,
        UserId userId,
        string? ratingComment = null)
    {
        var productRatingId = ProductRatingId.Create(id);
        var rating = Rating.Create(ratingValue);

        if (rating.IsError)
        {
            return rating.FirstError;
        }

        var productRating = new ProductRating(productRatingId,
            userId,
            rating.Value,
            ratingComment);
    
        productRating.AddDomainEvent(new ProductRatingUpdatedEvent(productRatingId));

        return productRating;
    }
}

