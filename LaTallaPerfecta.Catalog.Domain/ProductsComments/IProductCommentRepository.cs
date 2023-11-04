namespace LaTallaPerfecta.Catalog.Domain.ProductsComments;

public interface IProductCommentRepository
{
    Task AddAsync(ProductComment productComment);

    Task UpdateAsync(ProductComment productComment, ProductCommentId productCommentId);
}
