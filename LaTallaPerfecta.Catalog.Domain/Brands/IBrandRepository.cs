namespace LaTallaPerfecta.Catalog.Domain.Brands;

public interface IBrandRepository
{
    Task AddAsync(Brand brand);
}
