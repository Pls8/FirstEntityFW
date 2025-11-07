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
    public class ProductsClass
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Precision(18, 2)] // there is error without this in migration
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }


        // Foreign Key
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public CategoryClass Category { get; set; }


        // Navigation properties
        public ICollection<OrderItemClass> OrderItems { get; set; } = new List<OrderItemClass>();
        public ICollection<ReviewClass> Reviews { get; set; } = new List<ReviewClass>();
    }
}
