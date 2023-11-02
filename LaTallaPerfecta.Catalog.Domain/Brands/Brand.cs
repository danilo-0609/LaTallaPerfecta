using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Brands;

public sealed class Brand : AggregateRoot<BrandId, Ulid>
{
    public BrandId BrandId { get; private set; }
    
    public string Name { get; private set; }

    public string Email { get; private set; }

    public string WebsiteUrl { get; set; } = string.Empty;

    public string InstagramUrl { get; set; } = string.Empty;

    public string WhatsappUrl { get; set; } = string.Empty;

    public string TiktokUrl { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool isActive { get; set; }

    public int _amountOfSales { get; set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime UpdatedDateTime { get; private set; }


    private Brand(BrandId id) 
        : base(id)
    {
    }
}
