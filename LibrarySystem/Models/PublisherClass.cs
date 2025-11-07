using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class PublisherClass
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PublisherName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        // Navigation property
        public ICollection<BookClass> Books { get; set; }
    }
}
