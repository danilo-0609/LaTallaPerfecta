﻿using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Brands;
using LaTallaPerfecta.Catalog.Domain.Products.Events;
using LaTallaPerfecta.Catalog.Domain.Products.ValueObjects;
using LaTallaPerfecta.Catalog.Domain.ProductsComments;
using LaTallaPerfecta.Catalog.Domain.Ratings;
using LaTallaPerfecta.Catalog.Domain.Sellers;
using Microsoft.AspNetCore.Http;

namespace LaTallaPerfecta.Catalog.Domain.Products;

public sealed class Product : AggregateRoot<ProductId, Ulid>
{
    private readonly List<ProductCommentId> _commentIds = new();
    private readonly List<ProductRatingId> _ratingIds = new();

    public new Ulid Id { get; private set; }

    public SellerId SellerId { get; private set; }

    public ProductName Name { get; private set; }

    public Price Price { get; private set; }

    public Description Description { get; private set; }

    public BrandId? BrandId { get; private set; } 

    public string Size { get; private set; } = string.Empty;

    public string Color { get; private set; } = string.Empty;

    public ProductType ProductType { get; private set; }

    public Image Image { get; private set; }

    public int InStock { get; private set; }

    public bool IsActive { get; private set; }

    public IReadOnlyList<ProductCommentId> CommentIds => _commentIds.AsReadOnly();

    public IReadOnlyList<ProductRatingId> RatingIds => _ratingIds.AsReadOnly();

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }

    public DateTime? ExpireDateTime { get; private set;} 

    public static ErrorOr<Product> Create(Ulid sellerIdValue,
                                          string nameValue,
                                          decimal priceValue,
                                          ProductType productType,
                                          string imageUrlValue,
                                          IFormFile imageValue,
                                          int inStock,
                                          List<ProductCommentId> commentIds,
                                          List<ProductRatingId> ratingIds,
                                          string descriptionValue = "",
                                          BrandId? brandId = null,
                                          string size = "",
                                          string color = "")
    {
        var name = ProductName.Create(nameValue);
        var price = Price.Create(priceValue);
        var image = Image.Create(imageUrlValue, imageValue);
        var description = Description.Create(descriptionValue);

        List<IErrorOr> valueObjectsCheck = new();

        valueObjectsCheck.Add(name);
        valueObjectsCheck.Add(price);
        valueObjectsCheck.Add(image);
        valueObjectsCheck.Add(description);

        bool thereAreErrors = valueObjectsCheck
            .Any(valueObject => valueObject.IsError);

        if (thereAreErrors)
        {
            var errors = valueObjectsCheck
                .Select(valueObject => valueObject.Errors!
                .First());

            var firstError = errors.First();

            return firstError;
        }

        var productId = ProductId.CreateUnique();
        var sellerId = SellerId.Create(sellerIdValue);

        var product = new Product(productId,
            sellerId,
            name.Value, 
            price.Value, 
            description.Value, 
            brandId, 
            size, 
            color, 
            productType, 
            image.Value, 
            inStock,
            commentIds,
            ratingIds,
            DateTime.UtcNow);

        product.AddDomainEvent(new ProductCreatedEvent(productId));

        return product;
    }

    public static ErrorOr<Product> Update(Ulid id,
                                          Ulid sellerIdValue,
                                          string nameValue,
                                          decimal priceValue,
                                          ProductType productType,
                                          string imageUrlValue,
                                          IFormFile imageValue,
                                          int inStock,
                                          List<ProductCommentId> commentIds,
                                          List<ProductRatingId> ratingIds,
                                          DateTime createdDateTime,
                                          string descriptionValue = "",
                                          BrandId? brandId = null,
                                          string size = "",
                                          string color = "")
    {
        var name = ProductName.Create(nameValue);
        var price = Price.Create(priceValue);
        var image = Image.Create(imageUrlValue, imageValue);
        var description = Description.Create(descriptionValue);

        List<IErrorOr> valueObjectsCheck = new();

        valueObjectsCheck.Add(name);
        valueObjectsCheck.Add(price);
        valueObjectsCheck.Add(image);
        valueObjectsCheck.Add(description);

        bool thereAreErrors = valueObjectsCheck
            .Any(valueObject => valueObject.IsError);

        if (thereAreErrors)
        {
            var errors = valueObjectsCheck
                .Select(valueObject => valueObject.Errors!
                .First());

            var firstError = errors.First();

            return firstError;
        }

        var productId = ProductId.Create(id);
        var sellerId = SellerId.Create(sellerIdValue);
        
        var product = new Product(productId,
                sellerId,
                name.Value,
                price.Value,
                description.Value,
                brandId,
                size,
                color,
                productType,
                image.Value,
                inStock,
                commentIds,
                ratingIds,
                createdDateTime,
                DateTime.UtcNow);

        product.AddDomainEvent(new ProductUpdatedEvent(productId));

        return product;
    }

    public static void Expire(ProductId productId, Product product)
    {
        ProductExpiredEvent productExpiredEvent = new(productId);

        product.ExpireDateTime = DateTime.UtcNow;
        product.IsActive = false;
        product.AddDomainEvent(productExpiredEvent);
    }

    private Product(ProductId id,
        SellerId sellerId,
        ProductName name, 
        Price price, 
        Description description,
        BrandId? brandId,
        string size, 
        string color, 
        ProductType productType, 
        Image image, 
        int inStock,
        List<ProductCommentId> commentIds,
        List<ProductRatingId> ratingIds,
        DateTime createdDateTime,
        DateTime? updatedDateTime = null,
        DateTime? expiredDateTime = null)
            : base(id)
    {
        SellerId = sellerId;
        Name = name;
        Price = price;
        Description = description;
        BrandId = brandId;
        Size = size;
        Color = color;
        ProductType = productType;
        Image = image;
        InStock = inStock;

        _commentIds = commentIds;
        _ratingIds = ratingIds;

        IsActive = true;

        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        ExpireDateTime = expiredDateTime;
    }

    
    public sealed class Builder
    {
        private Ulid _sellerId { get; }

        private string _name { get; }

        private decimal _price { get; }

        private ProductType _productType { get; set; }

        private string _imageUrl { get; }

        private IFormFile _imageFile { get; }

        private int _inStock { get; }

        private string _description { get; set; } = string.Empty;

        private string _size { get; set; } = string.Empty;

        private string _color { get; set; } = string.Empty;

        private BrandId? _brand { get; set; }

        private List<ProductCommentId> _commentIds { get; set; }

        private List<ProductRatingId> _ratingIds { get; set; }


        public Builder(Ulid sellerId,
                       string name, 
                       decimal price, 
                       string size,
                       string imageUrl,
                       IFormFile imageFile,
                       int inStock,
                       List<ProductCommentId> commentIds,
                       List<ProductRatingId> ratingIds,
                       ProductType productType)
        {
            _sellerId = sellerId;
            _name = name;
            _price = price;
            _size = size;
            _imageUrl = imageUrl;
            _imageFile = imageFile;
            _inStock = inStock;
            _productType = productType;

            _commentIds = commentIds;
            _ratingIds = ratingIds;
        }

        public Builder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public Builder WithColor(string color)
        {
            _color = color;
            return this;
        }

        public Builder WithBrand(BrandId brand)
        {
            _brand = brand;
            return this;
        }

        public ErrorOr<Product> Build()
        {
            var product = Product.Create(_sellerId,
                _name,
                _price,
                _productType,
                _imageUrl,
                _imageFile,
                _inStock,
                _commentIds,
                _ratingIds,
                _description,
                _brand,
                _size,
                _color);

            if (product.IsError)
            {
                return product.FirstError;
            }

            return product;
        }
    }
}
