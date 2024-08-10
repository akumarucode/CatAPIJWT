using CatAPI2.Models;

namespace CatAPI2.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();

        Country GetCountry(int id);

        bool CountryExists(int id);

        ICollection<Owner> GetOwnersByCountry(int countryId);

        bool CreateCountry(Country country);

        bool Save();

        bool DeleteCountry(Country country);

        bool UpdateCountry(Country country);
    }
}

