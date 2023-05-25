using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;

namespace PetShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<IList<Product>> List()
        {
            return await _context.Product.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Product.FirstOrDefaultAsync(p => id == p.Id);
        }

        public async Task Delete(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task Update(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
