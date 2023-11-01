using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Products;
using LaTallaPerfecta.Catalog.Domain.Sellers.ValueObjects;

namespace LaTallaPerfecta.Catalog.Domain.Sellers;

public sealed class Seller : Entity<SellerId, Ulid>
{
    private readonly List<ProductId> _productsOnSale = new();

    public SellerId SellerId { get; private set; }

    public string Address { get; private set; }

    public SellerName SellerName { get; private set; }

    public string Email { get; private set; }

    public string ContactInformation { get; private set; }

    public string Description { get; private set; }

    public bool IsActive { get; private set; }

    public int AmountOfSales { get; private set; }

    public IReadOnlyList<ProductId> ProductsOnSale => _productsOnSale.AsReadOnly();
    
    public string ProfilePhotoUrl { get; private set; }

    private string _firstName { get; }

    private string _lastName { get; }

    private Seller(SellerId sellerId, 
        List<ProductId> productsOnSale, 
        string address, 
        SellerName sellerName, 
        string email, 
        string contactInformation, 
        string description, 
        bool isActive, 
        int amountOfSales, 
        string profilePhotoUrl, 
        string firstName, 
        string lastName)
            : base(sellerId)
    {
        _productsOnSale = productsOnSale;
        SellerId = sellerId;
        Address = address;
        SellerName = sellerName;
        Email = email;
        ContactInformation = contactInformation;
        Description = description;
        IsActive = isActive;
        AmountOfSales = amountOfSales;
        ProfilePhotoUrl = profilePhotoUrl;
        _firstName = firstName;
        _lastName = lastName;
    }

    //public static ErrorOr<Seller> Create();
    //public static ErrorOr<Seller> Update();
    //public builder

}
