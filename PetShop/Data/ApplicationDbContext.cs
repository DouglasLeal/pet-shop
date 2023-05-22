using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<PersonViewModel> Persons { get; set; }
        public DbSet<PersonViewModel> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PetShop.Models.Product> Product { get; set; } = default!;
    }
}