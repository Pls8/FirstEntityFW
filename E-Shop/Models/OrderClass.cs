using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class OrderClass
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";

        // Foreign Key
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public CustomerClass Customer { get; set; }

        //Many
        public ICollection<OrderItemClass> OrderItems { get; set; } = new List<OrderItemClass>();
    }
}
