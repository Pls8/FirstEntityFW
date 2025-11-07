using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LibrarySystem.Models
{
    public class BookClass
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; }

        public int PublicationYear { get; set; }

        [MaxLength(20)]
        public string AvailabilityStatus { get; set; } = "Available";

        // Foreign Key
        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }
        public PublisherClass Publisher { get; set; }


        // Foreign Key for Library Branch
        [ForeignKey(nameof(LibraryBranch))]
        public int LibraryBranchId { get; set; }
        public LibraryBranchClass LibraryBranch { get; set; }


        public ICollection<TransactionClass> Transactions { get; set; }
        public ICollection<BookAuthorClass> BookAuthors { get; set; }
        public ICollection<BookCategoryClass> BookCategories { get; set; }
    }
}
