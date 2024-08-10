using CatAPI2.Models;

namespace CatAPI2.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int id);

        bool OwnerExists(int id);

        ICollection<Cat> GetCatsByOwner(int ownerId);

        bool CreateOwner(int countryId, Owner owner);

        bool Save();

        bool DeleteOwner(Owner owner);

        bool UpdateOwner(Owner owner);
    }
}
