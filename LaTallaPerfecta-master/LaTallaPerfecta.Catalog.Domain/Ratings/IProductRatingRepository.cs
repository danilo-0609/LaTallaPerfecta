using System;

namespace LaTallaPerfecta.Catalog.Domain.Ratings;

public interface IProductRatingRepository
{
    Task AddAsync(ProductRating productRating);

    Task UpdateAsync(ProductRatingId productRatingId, ProductRating productRating);        
}

