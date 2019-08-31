namespace Models
{
    public class ToDoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
    }
}
