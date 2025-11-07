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
    public class LibraryMemberClass
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(20)]
        public string MembershipStatus { get; set; } = "Active";

        // Foreign Key
        [ForeignKey(nameof(LibraryBranch))]
        public int LibraryBranchId { get; set; }
        public LibraryBranchClass LibraryBranch { get; set; }


        public ICollection<TransactionClass> Transactions { get; set; }
    }
}
