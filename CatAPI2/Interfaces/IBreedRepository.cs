using CatAPI2.Models;

namespace CatAPI2.Interfaces
{
    public interface IBreedRepository
    {
        ICollection<Breed> GetBreeds();

        Breed GetBreed(int id);

        Breed GetBreedByName(string name);    

        bool BreedExists(int id); 
        bool BreedExistsByName(string name);

        Breed GetBreedByCat(int catId);

        ICollection<Cat> GetCatsByBreed(int breedId);

        bool CreateBreed(Breed breed);

        bool DeleteCategory(Breed breed);

        bool Save();

        bool UpdateBreed(Breed breed);
    }
}
