using Library_Management_Application.Entities.Common;

namespace Library_Management_Application.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public int PublishedYear { get; set; }
        public List<Author> Authors { get; set; }
        public bool IsBorrowed { get; set; }
    }
}
