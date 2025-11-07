using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class CategoryClass
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        // Navigation property for Many-to-Many relationship
        public ICollection<BookCategoryClass> BookCategories { get; set; }
    }
}
