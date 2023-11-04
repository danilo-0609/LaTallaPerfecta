using ErrorOr;

namespace LaTallaPerfecta.Catalog.Domain.Products.DomainErrors;

public static class ProductErrors
{
    public static class Products
    {

    }

    public static class Images
    {
        public static Error InvalidFormat(string message) =>
                    Error.Validation("Image.InvalidFormat", message);

        public static Error ExcessiveSize(string message) =>
            Error.Validation("Image.ExcessiveSize", message);
    }

    public static class Price
    {
        public static Error PriceEqualsToZero(string message) =>
            Error.Validation("Price.EqualToZero", message);

        public static Error PriceOverExpensive(string message) =>
            Error.Validation("Price.OverExpensive", message);
    }

    public static class Name
    {
        public static Error NameLengthOver250Letters(string message) =>
            Error.Validation("Name.VeryExtensive", message);

        public static Error NameLengthLessThan10WordsLetters(string message) =>
            Error.Validation("Name.VeryShort", message);
    }

    public static class Description
    {
        public static Error DescriptionLengthOver5000Letters(string message) =>
            Error.Validation("Description.VeryExtensive", message);
    }
}
