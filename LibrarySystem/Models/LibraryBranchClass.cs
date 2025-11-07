using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class LibraryBranchClass
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BranchName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }



        // Navigation properties (One-to-Many relationships)
        public ICollection<LibraryStaffClass> StaffMembers { get; set; }
        public ICollection<LibraryMemberClass> Members { get; set; }
        public ICollection<BookClass> Books { get; set; }
    }
}
