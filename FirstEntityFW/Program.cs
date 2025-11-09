using FirstEntityFW.Context;
using FirstEntityFW.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace FirstEntityFW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //____________________________________________________________
            // right-Click on Solution > Mange NuGet Packages > install   |
            // Microsfot.EnitiyFWCore 8.0.21                              |
            //          .EntitiyFWCore.SqlServer                          |
            //          .EnitityFWCore.Tool                               |
            //AppDbContext config                                         |
            //------------------------------------------------------------

            AppDbContext _context = new AppDbContext(); // addd instance of appDb
            UserRoleClass roles = new UserRoleClass();
            UserClass user = new UserClass();
            roles.role = "admin";
            _context.Roles.Add(roles); // Role table
            user.Name = "name";
            user.Email = "email";
            user.Password = "password";
            user.userRole = roles;
            _context.Users.Add(user); // Role table
            _context.SaveChanges();

            //var roles = appDb.Roles.Include(ui => ui.role).ToList();
            //foreach (var item in roles)
            //{
            //    Console.WriteLine($"{item.Id} | {item.role}");
            //}

            var search = (from u in _context.Users
                          where u.Id == 2
                          select u).FirstOrDefault();
            search.Name = "Updated Name";
            search.Email = "Updated Email";
            search.Password = "password";
            _context.Update(search);


            var users = _context.Users.Include(ui => ui.userRole).ToList();
            foreach (var item in users)
            {
                Console.WriteLine($"{item.Id} | {item.userRole.role} | {item.Name}");
            }




            //appDb.Roles.Remove(roles[1]);
            //appDb.SaveChanges();

            //role.Id = 2;
            //role.users = "user";

            //UserClass user = new UserClass();
            //user.Id = 1;
            //user.Name = "Test";
            //user.Email = "Test";
            //user.Password = "Test";
            //user.UserRole = 1;








        }
    }
}
