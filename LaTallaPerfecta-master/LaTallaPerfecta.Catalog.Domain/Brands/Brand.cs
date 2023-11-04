using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Brands.Events;
using LaTallaPerfecta.Catalog.Domain.Products;

namespace LaTallaPerfecta.Catalog.Domain.Brands;

public sealed class Brand : AggregateRoot<BrandId, Ulid>
{
    private readonly List<ProductId> _productIds = new();

    public BrandId BrandId { get; private set; }
    
    public string Name { get; private set; }

    public string Email { get; private set; }

    public string? WebsiteUrl { get; private set; } 

    public string? InstagramUrl { get; private set; } 

    public string? WhatsappUrl { get; private set; } 

    public string? TiktokUrl { get; private set; } 

    public string Description { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public int AmountOfSales { get; private set; }


    public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();


    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }

    

    private Brand(BrandId id,
    string name, 
    string email,
    string? websiteUrl,
    string? instagramUrl,
    string? whatsappUrl,
    string? tiktokUrl,
    DateTime createdDateTime,
    List<ProductId> productIds,
    string description = "",
    bool isActive = true,
    int amountOfSales = 0,
    DateTime? updatedDateTime = null) 
        : base(id)
    {
        BrandId = id;
        Name = name;
        Email = email;
        WebsiteUrl = websiteUrl;
        InstagramUrl = instagramUrl;
        WhatsappUrl = whatsappUrl;
        TiktokUrl = tiktokUrl;
        Description = description;
        AmountOfSales = amountOfSales;

        _productIds = productIds;

        IsActive = isActive;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Brand Create(string name, 
        string email,
        List<ProductId> productIds,
        string? websiteUrl = null,
        string? instagramUrl = null,
        string? whatsappUrl = null,
        string? tiktokUrl = null,
        string description = "")
    {
        BrandId brandId = BrandId.CreateUnique();

        var brand = new Brand(brandId,
            name,
            email,
            websiteUrl,
            instagramUrl,
            whatsappUrl,
            tiktokUrl,
            DateTime.UtcNow,
            productIds,
            description,
            true,
            0,
            null);

        brand.AddDomainEvent(new BrandCreatedEvent(brandId));

        return brand;
    }

    public static Brand Update(Ulid id,
        string name,
        string email,
        DateTime createdDateTime,
        int amountOfSales,
        List<ProductId> productIds,
        string? websiteUrl = null,
        string? instagramUrl = null,
        string? whatsappUrl = null,
        string? tiktokUrl = null,
        string description = "")
    {
        var brandId = BrandId.Create(id);

        var brand = new Brand(brandId,
            name, 
            email,
            websiteUrl,
            instagramUrl,
            whatsappUrl,
            tiktokUrl,
            createdDateTime,
            productIds,
            description,
            true,
            amountOfSales,
            DateTime.UtcNow);

        brand.AddDomainEvent(new BrandUpdatedEvent(brandId));

        return brand;
    }

    public static void Expire(Brand brand)
    {
        BrandExpiredEvent brandExpiredEvent = new(brand.BrandId);

        brand.AddDomainEvent(brandExpiredEvent);

        brand.IsActive = false;
    }

    public class Builder 
    {
        private Ulid _brandId { get; set; }
    
        private string _name { get; set; }

        private string _email { get; set; }

        private string? _websiteUrl { get; set; } 

        private string? _instagramUrl { get; set; } 

        private string? _whatsappUrl { get; set; } 

        private string? _tiktokUrl { get; set; } 

        private string _description { get; set; } = string.Empty;

        private bool _isActive { get; set; }

        private int _amountOfSales { get; set; }

        private List<ProductId> _productIds { get; set; }
        private DateTime _createdDateTime { get; set; }

        public Builder(Ulid id,
            string name,
            string email,
            DateTime createdDateTime,
            List<ProductId> productIds,
            string description = "")
        {
            _brandId = id;
            _name = name;
            _email = email;
            _createdDateTime = createdDateTime;
            _description = description;
            _productIds = productIds;
        }

        public Builder WithWebsiteUrl(string websiteUrl)
        {
            _websiteUrl = websiteUrl;
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

        public Builder WithTiktokUrl(string tiktokUrl)
        {
            _tiktokUrl = tiktokUrl;
            return this;
        }

        public Builder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public Builder WithCreatedDateTime(DateTime createdDateTime)
        {
            _createdDateTime = createdDateTime;
            return this;
        }

        public Builder WithAmountOfSales(int amountOfSales)
        {
            _amountOfSales = amountOfSales;
            return this;
        }

        public Brand Build()
        {
            var brand = Create(_name,
                _email,
                _productIds,
                _websiteUrl,
                _instagramUrl,
                _whatsappUrl,
                _tiktokUrl,
                _description);

                return brand;
        }

        public Brand BuildUpdate()
        {
            var brand = Update(_brandId,
                _name,
                _email,
                _createdDateTime,
                _amountOfSales,
                _productIds,
                _websiteUrl,
                _instagramUrl,
                _whatsappUrl,
                _tiktokUrl,
                _description);

            return brand;
        }

    }
}
