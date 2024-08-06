namespace CatAPI2.Models
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Cat> Cats { get; set; }
    }
}
