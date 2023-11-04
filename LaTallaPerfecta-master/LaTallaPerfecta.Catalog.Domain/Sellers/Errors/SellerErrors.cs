using ErrorOr;

namespace LaTallaPerfecta.Catalog.Domain.Sellers.Errors;

public static class SellerErrors
{
    public static class Name
    {
        public static Error NameLengthVeryExtensive(string message) =>
            Error.Validation("Seller.NameVeryExtensive", message);

        public static Error NameEmpty(string message) =>
            Error.Validation("Seller.NameEmpty", message);

    }
}
