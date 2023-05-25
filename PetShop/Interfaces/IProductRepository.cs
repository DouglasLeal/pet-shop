using PetShop.Models;

namespace PetShop.Interfaces
{
    public interface IProductRepository
    {
        Task Create(Product product);
        Task<IList<Product>> List();
        Task<Product> GetById(int id);
        Task Update(Product product);
        Task Delete(int id);
    }
}
