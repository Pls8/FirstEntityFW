using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class ReviewClass
    {
        [Key]
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        // Foreign Keys
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public ProductsClass Product { get; set; }


        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public CustomerClass Customer { get; set; }
    }
}
