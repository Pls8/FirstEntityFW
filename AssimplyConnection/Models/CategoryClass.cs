using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssimplyConnection.Models
{
    public class CategoryClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<ProductClass> Products { get; set; } = new List<ProductClass>();
    }
}
