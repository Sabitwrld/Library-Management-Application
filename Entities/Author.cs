using Library_Management_Application.Entities.Common;

namespace Library_Management_Application.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
