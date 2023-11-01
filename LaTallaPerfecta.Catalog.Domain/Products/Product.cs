using ErrorOr;
using LaTallaPerfecta.BuildingBlocks.Domain;
using LaTallaPerfecta.Catalog.Domain.Products.Events;
using LaTallaPerfecta.Catalog.Domain.Products.ValueObjects;
using LaTallaPerfecta.Catalog.Domain.ProductsType;
using Microsoft.AspNetCore.Http;

namespace LaTallaPerfecta.Catalog.Domain.Products;

public sealed class Product : AggregateRoot<ProductId, Ulid>
{
    //public UserId OwnerId { get; private set; }
    
    public new Ulid Id { get; private set; }

    public ProductName Name { get; private set; }

    public Price Price { get; private set; }

    public Description Description { get; private set; } 

    public string Brand { get; private set; } = string.Empty;

    public string Size { get; private set; } = string.Empty;

    public string Color { get; private set; } = string.Empty;

    public ProductType ProductType { get; private set; }

    public Image Image { get; private set; }

    public int InStock { get; private set; }

    public bool IsActive { get; private set; }

    public static ErrorOr<Product> Create(string nameValue,
                                          decimal priceValue,
                                          ProductType productType,
                                          string imageUrlValue,
                                          IFormFile imageValue,
                                          int inStock,
                                          string descriptionValue = "",
                                          string brand = "",
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

        var product = new Product(productId, 
            name.Value, 
            price.Value, 
            description.Value, 
            brand, 
            size, 
            color, 
            productType, 
            image.Value, 
            inStock);

        product.AddDomainEvent(new ProductCreatedEvent(productId));

        return product;
    }

    public static ErrorOr<Product> Update(Ulid id,
                                          string nameValue,
                                          decimal priceValue,
                                          ProductType productType,
                                          string imageUrlValue,
                                          IFormFile imageValue,
                                          int inStock,
                                          string descriptionValue = "",
                                          string brand = "",
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
        
        var product = new Product(productId,
                name.Value,
                price.Value,
                description.Value,
                brand,
                size,
                color,
                productType,
                image.Value,
                inStock);

        product.AddDomainEvent(new ProductUpdatedEvent(productId));

        return product;
    }

    public static void Expire(ProductId productId, Product product)
    {
        ProductExpiredEvent productExpiredEvent = new(productId);

        product.AddDomainEvent(productExpiredEvent);
    }

    private Product(ProductId id, 
                    ProductName name, 
                    Price price, 
                    Description description, 
                    string brand,
                    string size, 
                    string color, 
                    ProductType productType, 
                    Image image, 
                    int inStock)
        : base(id)
    {
        Name = name;
        Price = price;
        Description = description;
        Brand = brand;
        Size = size;
        Color = color;
        ProductType = productType;
        Image = image;
        InStock = inStock;

        IsActive = true;
    }

    
    public sealed class Builder
    {
        private string _name { get; }

        private decimal _price { get; }

        private ProductType _productType { get; set; }

        private string _imageUrl { get; }

        private IFormFile _imageFile { get; }

        private int _inStock { get; }

        private string _description { get; set; } = string.Empty;

        private string _size { get; set; } = string.Empty;

        private string _color { get; set; } = string.Empty;

        private string _brand { get; set; } = string.Empty;


        public Builder(string name, 
                       decimal price, 
                       string size,
                       string imageUrl,
                       IFormFile imageFile,
                       int inStock,
                       ProductType productType)
        {
            _name = name;
            _price = price;
            _size = size;
            _imageUrl = imageUrl;
            _imageFile = imageFile;
            _inStock = inStock;
            _productType = productType;
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

        public Builder WithBrand(string brand)
        {
            _brand = brand;
            return this;
        }

        public ErrorOr<Product> Build()
        {
            var product = Product.Create(_name, 
                _price, 
                _productType, 
                _imageUrl, 
                _imageFile, 
                _inStock, 
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
