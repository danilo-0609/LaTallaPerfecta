using LaTallaPerfecta.BuildingBlocks.Domain;
using Microsoft.AspNetCore.Http;

namespace LaTallaPerfecta.Catalog.Domain.Products.Rules;

internal sealed class ImageSizeMustBeLessThan10MbRule : IBusinessRule
{
    private const long MaxFileSize = 10 * 1024 * 1024;
    private readonly IFormFile _formFile;

    internal ImageSizeMustBeLessThan10MbRule(IFormFile formFile)
    {
        _formFile = formFile;
    }

    public string Message => 
                "The image size is larger than allowed. Must be less than 10MB";

    public bool IsBroken()
    {
        if (_formFile.Length > MaxFileSize)
        {
            return true;
        }

        return false;
    }
}
