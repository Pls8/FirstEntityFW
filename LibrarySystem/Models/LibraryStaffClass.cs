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
    public class LibraryStaffClass
    {
        [Key]
        public int StaffId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }

        // Foreign Key
        [ForeignKey(nameof(LibraryBranch))]
        public int StaffBranchId { get; set; }
        public LibraryBranchClass LibraryBranch { get; set; }

        public ICollection<TransactionClass> ProcessedTransactions { get; set; }
    }
}
