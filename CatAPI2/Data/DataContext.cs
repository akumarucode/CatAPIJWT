using CatAPI2.Models;
using Microsoft.EntityFrameworkCore;

namespace CatAPI2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {

        }

        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
