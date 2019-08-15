using System;

namespace Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string Genre { get; set; }

        public Movie(string name, int length, string genre, int id)
        {
            Name = name;
            Length = length;
            Genre = genre;
            Id = id;
        }

        public Movie()
        {
        }
    }
}
