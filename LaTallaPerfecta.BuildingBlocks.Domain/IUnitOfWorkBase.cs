namespace LaTallaPerfecta.BuildingBlocks.Domain;

public interface IUnitOfWorkBase 
{
    Task SaveChangesAsync();
}
