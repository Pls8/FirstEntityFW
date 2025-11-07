using FirstEntityFW.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEntityFW.Models.Auth
{
    public class UserClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Plan plan { get; set; } = Plan.free; // you can assign defualt value here!!!


        //In this side relationship O N E, use instance
        [ForeignKey(nameof(UserRole))]
        public int UserRole { get; set; }
        public UserRoleClass userRole { get; set; }


    }
}
