namespace EFCore6.Core.Entities
{
    public class Author
    {
        public Author()
        {
            Books = new List<Book>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // 1 to many
        public List<Book> Books { get; set; }
    }
}
