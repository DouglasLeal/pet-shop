using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;

namespace PetShop.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Animal animal)
        {
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<IList<Animal>> List()
        {
            return await _context.Animals.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<IList<Animal>> ListByOwner(int id)
        {
            return await _context.Animals.Where(a => a.PersonId == id).OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Animal?> GetById(int id)
        {
            return await _context.Animals.FirstOrDefaultAsync(a => id == a.Id);
        }

        public async Task Delete(Animal animal)
        {
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task Update(Animal animal)
        {
            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
