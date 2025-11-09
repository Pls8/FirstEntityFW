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
    {                        // ^__________v
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
            //_______________________________Connection Path_________________________________________
            // SQL server VIEW > SQL Server Object Explorer > (NameOFMachine) > right-click Proprties|
            // Categoriezed > Copy Conntion String >..                                               |
            //---------------------------------------|------------------------------------------------
            //     v--------------------------------<"
            string connection = "Data Source = . ; " +
                "database = TheMangerDB; " +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=True;" +
                "Trust Server Certificate=True;";

            optionsBuilder.UseSqlServer(connection);
        }




        //Class To Tables
        public DbSet<UserClass> Users { get; set; }
        public DbSet<UserRoleClass> Roles { get; set; }
                                //   ^----Table Name In DataBase





        //__________________< C A U T I O N > Note: cascade deletes_____________________________________
        //              Multiple relationships that all cascade deletes between the same tables         |
        // This is AI Fix for multiple relationships that all cascade deletes between the same tables <---.
        // The other way is to make ForeignKey Null ? <public int? StaffId { get; set; }>               | |
        // Do Not add-migration , Delete All Migration First > Remove-migration then new add-migration  | |
        //----------------------------------------------------------------------------------------------  |
        //protected override void OnModelCreating(ModelBuilder modelBuilder)// >----------------------------|
        //{
        //    modelBuilder.Entity<UserClass>()
        //        .HasOne(t => t.Email)
        //        .WithMany()
        //        .HasForeignKey(t => t.UserRole)
        //        .OnDelete(DeleteBehavior.Restrict);
        //    // .-------------------------^
        //    //_|__________________________________________________________________________________
        //    // |>Restrict : SQL will prevent deleting a member if there are related transactions. |
        //    // |>Cascade  : deleting the member deletes all related transactions.                 |
        //    // |>SetNull  : sets MemberId to NULL(requires int? MemberId).                        |
        //    //------------------------------------------------------------------------------------
        //}
    }
}
