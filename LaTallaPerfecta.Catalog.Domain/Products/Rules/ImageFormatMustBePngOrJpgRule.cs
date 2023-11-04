using LaTallaPerfecta.BuildingBlocks.Domain;
using Microsoft.AspNetCore.Http;

namespace LaTallaPerfecta.Catalog.Domain.Products.Rules;

internal sealed class ImageFormatMustBePngOrJpgRule : IBusinessRule
{
    private const string JpgFileType = "image/jpeg";
    private const string PngFileType = "image/png";
    private readonly IFormFile _imageFile;


    internal ImageFormatMustBePngOrJpgRule(IFormFile imageFile)
    {
        _imageFile = imageFile;
    }

    public string Message => "The image file type must be .jpg or .png";

    public bool IsBroken()
    {
        if(!_imageFile.ContentType.EndsWith(JpgFileType) &&
            !_imageFile.ContentType.EndsWith(PngFileType))
        {
            return true;
        }

        return false;
    }
}
