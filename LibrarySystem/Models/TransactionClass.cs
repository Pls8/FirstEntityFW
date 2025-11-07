using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class TransactionClass
    {
        [Key]
        public int TransactionId { get; set; }


        // Foreign Keys
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public BookClass Book { get; set; }

        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        [Precision(18, 2)]
        public decimal FineAmount { get; set; } = 0m;


        //                  < C A U T I O N > 
        //multiple relationships that all cascade deletes between the same tables//
        //fix in AppDbContext OR add _?_ e.g public int? StaffId_FK_ { get; set; }
        [ForeignKey(nameof(LibraryMember))]                                      //
        public int MemberId { get; set; }                                        //
        public LibraryMemberClass LibraryMember { get; set; }                    //
                                                                                 //
                                                                                 //
        [ForeignKey(nameof(LibraryStaff))]                                       //
        public int StaffId { get; set; }                                         //
        public LibraryStaffClass LibraryStaff { get; set; }                      //
        //-----------------------------------------------------------------------//


        
    }
}
