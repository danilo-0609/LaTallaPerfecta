using LaTallaPerfecta.BuildingBlocks.Domain;

namespace LaTallaPerfecta.Catalog.Domain.Sellers.ValueObjects;

public sealed record ContactInformation : ValueObject
{
    public string WebSiteUrl { get; private set; } = string.Empty;

    public string InstagramUrl { get; private set; } = string.Empty;

    public string WhatsAppUrl { get; private set; } = string.Empty;

    public string TikTokUrl { get; private set; } = string.Empty;
    
    private ContactInformation(string websiteUrl = "", 
        string instagramUrl = "", 
        string whatsappUrl = "",
        string tikTokUrl = "")
    {
        WebSiteUrl = websiteUrl;
        InstagramUrl = instagramUrl;
        WhatsAppUrl = whatsappUrl;
        TikTokUrl = tikTokUrl;
    }

    public static ContactInformation Create(string websiteUrl = "",
        string instagramUrl = "",
        string whatsappUrl = "",
        string tikTokUrl = "")
    {
        return new ContactInformation(websiteUrl, instagramUrl, whatsappUrl, tikTokUrl);
    }
}
