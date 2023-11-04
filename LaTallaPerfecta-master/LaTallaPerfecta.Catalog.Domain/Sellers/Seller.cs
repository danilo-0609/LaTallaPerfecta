using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Products;
using LaTallaPerfecta.Catalog.Domain.Sellers.Events;
using LaTallaPerfecta.Catalog.Domain.Sellers.ValueObjects;

namespace LaTallaPerfecta.Catalog.Domain.Sellers;

public sealed class Seller : AggregateRoot<SellerId, Ulid>
{
    private readonly List<ProductId> _productIds = new();

    public SellerId SellerId { get; private set; }

    public string Address { get; private set; } 

    public SellerName SellerName { get; private set; }

    public string Email { get; private set; } 

    public ContactInformation ContactInformation { get; private set; } 

    public string Description { get; private set; } 

    public bool IsActive { get; private set; } 

    public int AmountOfSales { get; private set; } 
   
    public string ProfilePhotoUrl { get; private set; }

    private string _firstName { get; }

    private string _lastName { get; }

    public IReadOnlyList<ProductId> ProductsIds => _productIds.AsReadOnly();

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }


    private Seller(SellerId sellerId, 
        string address, 
        SellerName sellerName, 
        string email, 
        ContactInformation contactInformation, 
        string description, 
        bool isActive, 
        int amountOfSales, 
        string profilePhotoUrl, 
        string firstName, 
        string lastName,
        List<ProductId> productIds,
        DateTime createdDateTime,
        DateTime? updatedDateTime = null)
                : base(sellerId)
    {
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
        _productIds = productIds;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static ErrorOr<Seller> Create(Ulid sellerId, 
        string address, 
        string email,
        string firstName,
        List<ProductId> productIds,
        string lastName = "",
        string profilePhotoUrl = "",
        int amountOfSales = 0,
        bool isActive = true,
        string description = "",
        string webSiteUrl = "", 
        string instagramUrl = "", 
        string whatsappUrl = "", 
        string tiktokUrl = "")
    {
        var sellerName = SellerName.Create($"{firstName} {lastName}");
    
        if (sellerName.IsError)
        {
            return sellerName.FirstError;
        }

        ContactInformation contactInformation = ContactInformation.Create(webSiteUrl, 
            instagramUrl, 
            whatsappUrl, 
            tiktokUrl);

        var seller = new Seller(SellerId.Create(sellerId), 
            address, 
            sellerName.Value, 
            email, 
            contactInformation, 
            description, 
            isActive, 
            amountOfSales, 
            profilePhotoUrl, 
            firstName, 
            lastName,
            productIds,
            DateTime.UtcNow);

        seller.AddDomainEvent(new SellerCreatedEvent(seller));

        return seller;
    }

    public static ErrorOr<Seller> Update(Ulid sellerId,
       string address,
       string email,
       string firstName,
       List<ProductId> productIds,
       DateTime createdDateTime,
       string lastName = "",
       string profilePhotoUrl = "",
       int amountOfSales = 0,
       bool isActive = true,
       string description = "",
       string webSiteUrl = "",
       string instagramUrl = "",
       string whatsappUrl = "",
       string tiktokUrl = "")
    {
        var sellerName = SellerName.Create($"{firstName} {lastName}");

        if (sellerName.IsError)
        {
            return sellerName.FirstError;
        }

        ContactInformation contactInformation = ContactInformation.Create(webSiteUrl,
            instagramUrl,
            whatsappUrl,
            tiktokUrl);

        var seller = new Seller(SellerId.Create(sellerId),
            address,
            sellerName.Value,
            email,
            contactInformation,
            description,
            isActive,
            amountOfSales,
            profilePhotoUrl,
            firstName,
            lastName,
            productIds,
            createdDateTime,
            DateTime.UtcNow);

        seller.AddDomainEvent(new SellerUpdatedEvent(seller));

        return seller;
    }

    public sealed class Builder
    {
        private Ulid _sellerId { get; set; }

        private string _address { get; set; }

        private string _email { get; set; }

        private string _websiteUrl { get; set; } = string.Empty;

        private string _instagramUrl { get; set; } = string.Empty;

        private string _whatsappUrl { get; set; } = string.Empty;

        private string _tiktokUrl { get; set; } = string.Empty;

        private string _description { get; set; } = string.Empty;

        private bool _isActive { get; set; } 

        private int _amountOfSales { get; set; }

        private string _profilePhotoUrl { get; set; } = string.Empty;

        private string _firstName { get; }

        private string _lastName { get; }

        private List<ProductId> _productIds { get; set; }

        private DateTime _createdDateTime { get; set; }

        private DateTime? _updatedDateTime { get; set; }

        public Builder(Ulid sellerId,
            string address,
            string email, 
            string firstName, 
            string lastName,
            List<ProductId> productIds,
            DateTime createdDateTime)
        {
            _sellerId = sellerId;
            _address = address;
            _email = email;
            _isActive = true;
            _amountOfSales = 0;
            _firstName = firstName;
            _lastName = lastName;
            _productIds = productIds;
            _createdDateTime = createdDateTime; 
        }

        public Builder WithTiktokUrl(string webSiteUrl)
        {
            _websiteUrl = webSiteUrl;
            return this;
        }

        public Builder WithInstagramUrl(string instagramUrl)
        {
            _instagramUrl = instagramUrl;
            return this;
        }

        public Builder WithWhatsappUrl(string whatsappUrl)
        {
            _whatsappUrl = whatsappUrl;
            return this;
        }

        public Builder WithWebSiteUrl(string tiktokUrl)
        {
            _tiktokUrl = tiktokUrl;
            return this;
        }

        public Builder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public Builder WithProfilePhotoUrl(string profilePhotoUrl)
        {
            _profilePhotoUrl = profilePhotoUrl;
            return this;
        }

        public Builder WithCreatedDateTime(DateTime createdDateTime)
        {
            _createdDateTime = createdDateTime;
            return this;
        }

        public ErrorOr<Seller> Build()
        {
            var seller = Create(_sellerId,
                _address,
                _email,

                _firstName,
                _productIds,
                _lastName,
                _profilePhotoUrl,
                _amountOfSales,
                _isActive,
                _description,
                _websiteUrl,
                _instagramUrl,
                _whatsappUrl,
                _tiktokUrl);

            if (seller.IsError)
            {
                return seller.FirstError;
            }

            return seller.Value;
        }

        public ErrorOr<Seller> BuildUpdate()
        {
            var seller = Update(_sellerId, _address,
            _email, 
            _firstName,
            _productIds,
            _createdDateTime,
            _lastName,
            _profilePhotoUrl,
            _amountOfSales,
            _isActive,
            _description,
            _websiteUrl,
            _instagramUrl,
            _whatsappUrl,
            _tiktokUrl);

            if (seller.IsError)
            {
                return seller.FirstError;
            }
        
            return seller.Value;
        }

    }
}
