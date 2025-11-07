using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class BookAuthorClass
    {
        [Key]
        public int BookAuthorId { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public BookClass Book { get; set; }


        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public AuthorClass Author { get; set; }
    }
}
