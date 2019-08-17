namespace Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }

        public Movie(int id, string name, int length)
        {
            Id = id;
            Name = name;
            Length = length;
        }

        public Movie()
        {
            
        }
    }
}
