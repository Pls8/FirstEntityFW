using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEntityFW.Models.Auth
{
    public class UserRoleClass
    {
        public int Id { get; set; }
        public string role { get; set; }

        //relationship in this side M A N Y
        public ICollection<UserClass> users = new List<UserClass>();
    }
}
