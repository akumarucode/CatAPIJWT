using CatAPI2.Models;

namespace CatAPI2.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int id);

        bool OwnerExists(int id);

        ICollection<Cat> GetCatsByOwner(int ownerId);
    }
}
