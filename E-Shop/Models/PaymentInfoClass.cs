using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class PaymentInfoClass
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }


        // Foreign Key
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public CustomerClass Customer { get; set; }


        //SELECT 
        //    COLUMN_NAME, 
        //    DATA_TYPE, 
        //    CHARACTER_MAXIMUM_LENGTH 
        //FROM INFORMATION_SCHEMA.COLUMNS
        //WHERE TABLE_NAME = 'PaymentInfos'-- your table name
        //AND COLUMN_NAME = 'CardNumber';  -- your column name
    }
}
