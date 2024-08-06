using CatAPI2.Data;
using CatAPI2.Interfaces;
using CatAPI2.Models;

namespace CatAPI2.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;

        }
        public bool CountryExists(int id)
        {
            return _context.Countries.Any(b => b.Id == id);
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(b => b.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersByCountry(int countryId)
        {
            return _context.Owners.Where(p => p.country.Id == countryId).ToList();
        }
    }
}
