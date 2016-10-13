using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2Blank.Data.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }

        public string Title { get; set; }
      
        public int AuthorID { get; set; }

        public short Rate { get; set; }

        [ForeignKey("AuthorID")]
        public virtual Author Author {get;set;}
    }
}
