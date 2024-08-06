using CatAPI2.Data;
using CatAPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CatAPI2.Models; // Adjust the namespace to match your project's structure

namespace CatAPI2
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            // Clear existing data
            _context.Breeds.RemoveRange(_context.Breeds);
            _context.Cats.RemoveRange(_context.Cats);
            _context.Countries.RemoveRange(_context.Countries);
            _context.Owners.RemoveRange(_context.Owners);

            _context.SaveChanges();

                var countries = new List<Country>
                {
                    new Country { Name = "USA" },
                    new Country { Name = "Canada" },
                    new Country { Name = "Japan" }
                };

                _context.Countries.AddRange(countries);
                _context.SaveChanges();
            

                var breeds = new List<Breed>
                {
                    new Breed { Name = "Siamese" },
                    new Breed { Name = "Persian" },
                    new Breed { Name = "Maine Coon" }
                };

                _context.Breeds.AddRange(breeds);
                _context.SaveChanges();
            

                var usa = _context.Countries.First(c => c.Name == "USA");
                var canada = _context.Countries.First(c => c.Name == "Canada");
                var japan = _context.Countries.First(c => c.Name == "Japan");

                var owners = new List<Owner>
                {
                    new Owner { Name = "Alice", country = usa },
                    new Owner { Name = "Bob", country = canada },
                    new Owner { Name = "Charlie", country = japan }
                };

                _context.Owners.AddRange(owners);
                _context.SaveChanges();
            

                var siamese = _context.Breeds.First(b => b.Name == "Siamese");
                var persian = _context.Breeds.First(b => b.Name == "Persian");
                var maineCoon = _context.Breeds.First(b => b.Name == "Maine Coon");

                var alice = _context.Owners.First(o => o.Name == "Alice");
                var bob = _context.Owners.First(o => o.Name == "Bob");
                var charlie = _context.Owners.First(o => o.Name == "Charlie");

                var cats = new List<Cat>
                {
                    new Cat { Name = "Whiskers", Age = 2, Owner = alice, breed = siamese },
                    new Cat { Name = "Fluffy", Age = 3, Owner = bob, breed = persian },
                    new Cat { Name = "Shadow", Age = 1, Owner = charlie, breed = maineCoon }
                };

                _context.Cats.AddRange(cats);
                _context.SaveChanges();
            
        }
    }
}
