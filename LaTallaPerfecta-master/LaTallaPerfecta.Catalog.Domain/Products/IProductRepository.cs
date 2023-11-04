namespace LaTallaPerfecta.Catalog.Domain.Products;

public interface IProductRepository 
{
    Task AddAsync(Product product);

    
}
