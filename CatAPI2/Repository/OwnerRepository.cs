using CatAPI2.Data;
using CatAPI2.Interfaces;
using CatAPI2.Models;

namespace CatAPI2.Repository
{
    public class OwnerRepository : IOwnerRepository
    {

        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;

        }

        public ICollection<Cat> GetCatsByOwner(int ownerId)
        {
            return _context.Cats.Where(p => p.Owner.Id == ownerId).ToList();
        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(b => b.Id).ToList();
        }

        public bool OwnerExists(int id)
        {
            return _context.Owners.Any(b => b.Id == id);
        }
    }
}
