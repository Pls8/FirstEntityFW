using FirstEntityFW.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEntityFW.Context
{
    public class AppDbContext : DbContext 
    {                          // ^__________v
        //_______________________________Instrucation________________________________________________
        // Tools > NuGet Package Manger Console,  PM> add-migration "msg" | PM> update-database      |
        // RollBack PM> update-database -m "RevsionName"                                             |
        // Return to initial status > update-database 0                                              |
        // in case of mulitple project in same solution you need to run like this for migration      |
        // Add-Migration <MigrationName> -Project <ProjectName> -StartupProject <StartupProjectName> |
        // PM> Add-Migration InitialCreate -Project E-Shop -StartupProject E-Shop                    |
        // Update-Database -Project "E-Shop" -StartupProject "E-Shop"                                |
        // Remove-Migration -Project LibrarySystem|                                                  |
        //-------------------------------------------------------------------------------------------
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //SQL server VIEW > SQL Server Object Explorer > (NameOFMachine) > right-click Proprties 
            // Categoriezed > Copy Conntion String 

            string connection = "Data Source = . ; " +
                "database = TheMangerDB; " +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=True;" +
                "Trust Server Certificate=True;";

            optionsBuilder.UseSqlServer(connection);
        }


        //here gose assign 
        public DbSet<UserClass> Users { get; set; }     //table
        public DbSet<UserRoleClass> Roles { get; set; } //table

        // Tools > NuGet Package Manger Console,  PM> add-migration "msg" | PM> update-database
        // RollBack PM> update-database -m "RevsionName"
        // Return to initial status > update-database 0
    }
}
