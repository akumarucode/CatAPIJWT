namespace CatAPI2.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country country { get; set; }

        public ICollection<Cat> Cats { get; set; }
    }
}
