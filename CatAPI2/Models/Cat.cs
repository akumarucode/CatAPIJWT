namespace CatAPI2.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public Owner Owner { get; set; }

        public Breed breed { get; set; }
    }
}
