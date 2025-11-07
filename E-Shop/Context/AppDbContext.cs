using E_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Context
{
    public class AppDbContext : DbContext
    {

        //_______________________________Instrucation________________________________________________
        // Tools > NuGet Package Manger Console,  PM> add-migration "msg" | PM> update-database      |
        // RollBack PM> update-database -m "RevsionName"                                             |
        // Return to initial status > update-database 0                                              |
        // in case of mulitple project in same solution you need to run like this for migration      |
        //Add-Migration <MigrationName> -Project <ProjectName> -StartupProject <StartupProjectName>  |
        //PM> Add-Migration InitialCreate -Project E-Shop -StartupProject E-Shop                     |
        //Update-Database -Project "E-Shop" -StartupProject "E-Shop"                                 |
        //Remove-Migration -Project LibrarySystem|                                                   |
        //-------------------------------------------------------------------------------------------

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = "Data Source=.; " +
                "database=Eshop;" + 
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=True;" +
                "Trust Server Certificate=True;";

            optionsBuilder.UseSqlServer(connection);
        }

        //  Tables = Class
        public DbSet<CustomerClass> Customers { get; set; }
        public DbSet<CategoryClass> Categories { get; set; }
        public DbSet<ProductsClass> Products { get; set; }
        public DbSet<OrderClass> Orders { get; set; }
        public DbSet<OrderItemClass> OrderItems { get; set; }
        public DbSet<ShippingAddressClass> ShippingAddresses { get; set; }
        public DbSet<PaymentInfoClass> PaymentInfos { get; set; }
        public DbSet<ReviewClass> Reviews { get; set; }


    }
}
