using CatAPI2.Models;

namespace CatAPI2.Interfaces
{
    public interface ICatRepository
    {
        ICollection<Cat> GetCats();

        Cat GetCat(int id);

        bool CatExists(int id);

        bool CreateCat (int ownerId, int breedId ,Cat cat);

        bool Save();
    }
}
