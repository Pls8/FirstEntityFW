using AssimplyConnection.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssimplyConnection.Context
{
    public class AppDbContext : DbContext
    {
        //_______________________________Instruction_________________________________________________
        // Tools > NuGet Package Manger Console,                                                     |
        //          ^--------> [ PM> add-migration "msg" ] | [ PM> update-database ]                 |
        // RollBack [ PM> update-database -m "MigrationName" ]                                       |
        // Return to initial status of db [ PM> update-database 0 ]                                  |
        //                                                                                           |
        // In case of multiple project in same solution you need to run like this for migration      |
        // Add-Migration <MigrationName> -Project <ProjectName> -StartupProject <StartupProjectName> |
        // [ PM> Add-Migration InitialCreate -Project E-Shop -StartupProject E-Shop ]                |
        // [ PM> Update-Database -Project "E-Shop" -StartupProject "E-Shop" ]                        |
        // [ PM> Remove-Migration -Project E-Shop ]                                                  |
        //-------------------------------------------------------------------------------------------

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = "Data Source = . ; " +
                "database = AssimplyConnection; " +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=True;" +
                "Trust Server Certificate=True;";

            optionsBuilder.UseSqlServer(connection);
        }


        public DbSet<ProductClass> Products { get; set; }
        public DbSet<CategoryClass> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the Config assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
