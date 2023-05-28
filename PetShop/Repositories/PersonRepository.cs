using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Enum;
using PetShop.Interfaces;
using PetShop.Models;

namespace PetShop.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<IList<Person>> List()
        {
            return await _context.Persons.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<IList<Person>> ListByType(PersonType type)
        {
            return await _context.Persons.Where(p => p.Type == type).OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Person?> GetById(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => id == p.Id);
        }

        public async Task Delete(Person person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task Update(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
