using PetShop.Enum;
using PetShop.Models;

namespace PetShop.Interfaces
{
    public interface IPersonRepository : IDisposable
    {
        Task Create(Person person);
        Task<IList<Person>> List();
        Task<IList<Person>> ListByType(PersonType type);
        Task<Person?> GetById(int id);
        Task Update(Person person);
        Task Delete(Person person);
    }
}
