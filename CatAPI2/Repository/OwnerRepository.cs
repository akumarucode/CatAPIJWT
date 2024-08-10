using CatAPI2.Data;
using CatAPI2.Interfaces;
using CatAPI2.Models;
using System.Diagnostics.Metrics;

namespace CatAPI2.Repository
{
    public class OwnerRepository : IOwnerRepository
    {

        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;

        }

        public bool CreateOwner(int countryId, Owner owner)
        {
            var ownerCountry = _context.Countries.Where(a => a.Id == countryId).FirstOrDefault();

            var ownerDetails = new Owner()
            {
                Name = owner.Name,
                country = ownerCountry,

            };

            _context.Add(ownerDetails);

            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }
    }
}
