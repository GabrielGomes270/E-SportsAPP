using E_SportsAPP.Models;

namespace E_SportsAPP.Repositories
{
    public interface IProductRepository
    {
        Task <IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(int id, Product product);
        Task DeleteProductAsync(int id);
    }
}
