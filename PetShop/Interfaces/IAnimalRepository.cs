using PetShop.Models;

namespace PetShop.Interfaces
{
    public interface IAnimalRepository : IDisposable
    {
        Task Create(Animal animal);
        Task<IList<Animal>> List();
        Task<IList<Animal>> ListByOwner(int id);
        Task<Animal?> GetById(int id);
        Task Update(Animal animal);
        Task Delete(Animal animal);
    }
}
