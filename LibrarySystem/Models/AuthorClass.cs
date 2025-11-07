using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class AuthorClass
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }


        // Navigation property for Many-to-Many relationship
        public ICollection<BookAuthorClass> BookAuthors { get; set; }
    }
}
