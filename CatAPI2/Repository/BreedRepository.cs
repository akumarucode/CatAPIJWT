using CatAPI2.Data;
using CatAPI2.Interfaces;
using CatAPI2.Models;

namespace CatAPI2.Repository
{
    public class BreedRepository : IBreedRepository
    {
        private readonly DataContext _context;

        public BreedRepository (DataContext context) 
        {
            _context = context;
           
        }

        public Breed GetBreed(int id)
        {
            return _context.Breeds.Where(b => b.Id == id).FirstOrDefault();
        }

        public Breed GetBreedByName(string name)
        {
            return _context.Breeds.Where(b => b.Name == name).FirstOrDefault();
        }

        public ICollection<Breed> GetBreeds()
        {
           return _context.Breeds.OrderBy(b => b.Id).ToList();  
        }

        public bool BreedExists(int id)
        {
            return _context.Breeds.Any(b => b.Id == id);
        }

        public bool BreedExistsByName(string name)
        {
            return _context.Breeds.Any(b => b.Name == name);
        }

        public Breed GetBreedByCat(int catId)
        {
            return _context.Cats.Where(o => o.Id == catId).Select(c => c.breed).FirstOrDefault();
        }

        public ICollection<Cat> GetCatsByBreed(int breedId)
        {
            return _context.Cats.Where(p => p.breed.Id == breedId).ToList();
        }

        public bool CreateBreed(Breed breed)
        {


            var breedDetails = new Breed()
            {
                Name = breed.Name,

            };

            _context.Add(breedDetails);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteCategory(Breed breed)
        {
            _context.Remove(breed);
            return Save();
        }

        public bool UpdateBreed(Breed breed)
        {
            _context.Update(breed);
            return Save();
        }
    }
}
