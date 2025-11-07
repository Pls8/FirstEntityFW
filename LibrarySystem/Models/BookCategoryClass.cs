using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class BookCategoryClass
    {
        [Key]
        public int BookCategorytId { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public BookClass Book { get; set; }



        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public CategoryClass Category { get; set; }
    }
}
