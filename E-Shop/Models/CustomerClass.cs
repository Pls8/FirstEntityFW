using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class CustomerClass
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Navigation properties (One-to-Many relation)
        public ICollection<OrderClass> Orders { get; set; } = new List<OrderClass>();
        public ICollection<ShippingAddressClass> ShippingAddresses { get; set; } = new List<ShippingAddressClass>();
        public ICollection<PaymentInfoClass> PaymentInfos { get; set; } = new List<PaymentInfoClass>();
        public ICollection<ReviewClass> Reviews { get; set; } = new List<ReviewClass>();
    }
}
