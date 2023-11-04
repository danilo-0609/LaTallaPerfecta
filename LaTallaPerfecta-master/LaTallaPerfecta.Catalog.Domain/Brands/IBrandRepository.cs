namespace LaTallaPerfecta.Catalog.Domain.Brands;

public interface IBrandRepository
{
    Task AddAsync(Brand brand);

    Task<bool> EmailIsUnique(string email);

    Task<bool> NameIsUnique(string name);
}
