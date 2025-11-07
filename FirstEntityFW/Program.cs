using FirstEntityFW.Context;
using FirstEntityFW.Models.Auth;

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

            AppDbContext appDb = new AppDbContext(); // addd instance of appDb
            UserRoleClass role = new UserRoleClass();
            //role.Id = 1; this will grant error
            role.role = "admin";
            appDb.Roles.Add(role); // Role table
            appDb.SaveChanges();

            var roles = appDb.Roles.ToList();
            foreach (var item in roles)
            {
                Console.WriteLine($"{item.Id} | {item.role}");
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
