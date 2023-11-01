using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Products.DomainErrors;
using LaTallaPerfecta.Catalog.Domain.Products.Rules;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LaTallaPerfecta.Catalog.Domain.Products.ValueObjects;

public sealed record Image : ValueObject
{    
    public string UrlValue { get; private set; }

    public IFormFile ImageFile { get; private set; }
    
    public static ErrorOr<Image> Create(string urlValue, IFormFile imageFile)
    {
        var checkingRules = CheckRules(imageFile);

        if (checkingRules.IsError)
        {
            return checkingRules.FirstError;
        }

        return new Image(urlValue, imageFile);

    }

    private static ErrorOr<Unit> CheckRules(IFormFile imageFile)
    {
        ImageFormatMustBePngOrJpgRule imageFormatRule = new(imageFile);

        if (imageFormatRule.IsBroken())
        {
            return ProductErrors.Images.InvalidFormat(imageFormatRule.Message);
        }

        ImageSizeMustBeLessThan10MbRule imageSizeRule = new(imageFile);

        if (imageSizeRule.IsBroken())
        {
            return ProductErrors.Images.ExcessiveSize(imageSizeRule.Message);
        }

        return Unit.Value;
    }

    private Image(string urlValue, IFormFile imageFile)
    {
        UrlValue = urlValue;
        ImageFile = imageFile;
    }
}
