using System.Collections.Generic;

namespace Angular2Blank.Data.Entities
{
    public class Author:BaseEntity
    {
        public Author()
        {
            Books = new List<Book>();
        }
        public string Name { get; set; }

        public string Age { get; set; }

        public short Rate { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}