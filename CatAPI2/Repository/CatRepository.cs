using CatAPI2.Data;
using CatAPI2.Interfaces;
using CatAPI2.Models;
using Microsoft.EntityFrameworkCore;

namespace CatAPI2.Repository
{
    public class CatRepository : ICatRepository
    {

        private readonly DataContext _context;

        public CatRepository(DataContext context)
        {
            _context = context;

        }

        public bool CatExists(int id)
        {
            return _context.Cats.Any(b => b.Id == id);
        }

        public bool CreateCat(int ownerId,int breedId, Cat cat)
        {
            var catOwner = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var catBreed = _context.Breeds.Where(a => a.Id == breedId).FirstOrDefault();


            var catDetails = new Cat()
            {
                Name = cat.Name,
                Age = cat.Age,
                Owner = catOwner,
                breed = catBreed,
            };

            _context.Add(catDetails);

            return Save();
        }

        public bool DeleteCat(Cat cat)
        {
            _context.Remove(cat);
            return Save();
        }

        public Cat GetCat(int id)
        {
            return _context.Cats.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<Cat> GetCats()
        {
            return _context.Cats.OrderBy(b => b.Id).ToList();
        }

        public bool Save()
        {
            var saved  = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCat(int ownerId , int breedId ,Cat cat)
        {
            var catOwner = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var catBreed = _context.Breeds.Where(a => a.Id == breedId).FirstOrDefault();
            var catDetails = new Cat()
            {
                Id = cat.Id,
                Name = cat.Name,
                Age = cat.Age,
                Owner = catOwner,
                breed = catBreed,
            };

            _context.Update(catDetails);
            return Save();
        }
    }
}
