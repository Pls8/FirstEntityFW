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
    public class OrderItemClass
    {
        [Key]
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal ItemPrice { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public OrderClass Order { get; set; }


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public ProductsClass Product { get; set; }
    }
}
