namespace Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Cinema()
        {
        }

        public Cinema(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
