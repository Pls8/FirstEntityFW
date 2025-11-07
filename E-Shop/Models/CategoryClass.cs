using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class CategoryClass
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }


        // Navigation property (One-to-Many relationship with Products)
        public ICollection<ProductsClass> Products { get; set; } = new List<ProductsClass>();
    }
}
