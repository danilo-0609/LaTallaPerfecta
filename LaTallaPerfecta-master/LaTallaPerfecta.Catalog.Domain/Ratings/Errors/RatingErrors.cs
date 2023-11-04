using ErrorOr;

namespace LaTallaPerfecta.Catalog.Domain.Ratings.Errors;

public static class RatingErrors 
{
    public static Error InvalidDouble(string message) => 
        Error.Validation("Rating.NotValid", message);
}
