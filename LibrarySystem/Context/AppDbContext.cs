using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Context
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
                                "database=Librarydb;" +
                                "Integrated Security=True;" +
                                "Connect Timeout=30;" +
                                "Encrypt=True;" +
                                "Trust Server Certificate=True;";

            optionsBuilder.UseSqlServer(connection);
        }


        //Tables = Class
        public DbSet<LibraryBranchClass> LibraryBranches { get; set; }
        public DbSet<LibraryStaffClass> LibraryStaff { get; set; }
        public DbSet<LibraryMemberClass> LibraryMembers { get; set; }
        public DbSet<PublisherClass> Publishers { get; set; }
        public DbSet<AuthorClass> Authors { get; set; }
        public DbSet<CategoryClass> Categories { get; set; }
        public DbSet<BookClass> Books { get; set; }
        public DbSet<TransactionClass> Transactions { get; set; }
        public DbSet<BookAuthorClass> BookAuthors { get; set; }
        public DbSet<BookCategoryClass> BookCategories { get; set; }



        //_________________________________Note: cascade deletes_______________________________________
        // This is AI Fix for multiple relationships that all cascade deletes between the same tables  |
        // The other way is to make ForeignKey Null ? <public int? StaffId { get; set; }>              |
        // Do Not add-migration , Delete All Migration First > Remove-migration then add new one       |
        //---------------------------------------------------------------------------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionClass>()
                .HasOne(t => t.LibraryMember)
                .WithMany() //.WithMany(m => m.Transactions)
                .HasForeignKey(t => t.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
                                      // Restrict : SQL will prevent deleting a member if there are related transactions.
                                      // Cascade : deleting the member deletes all related transactions.
                                      // SetNull : sets MemberId to NULL(requires int? MemberId).

            modelBuilder.Entity<TransactionClass>()
                .HasOne(t => t.Book)
                .WithMany()
                .HasForeignKey(t => t.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionClass>()
                .HasOne(t => t.LibraryStaff)
                .WithMany()
                .HasForeignKey(t => t.StaffId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LibraryMemberClass>()
                .HasMany(m => m.Transactions)
                .WithOne(t => t.LibraryMember)
                .HasForeignKey(t => t.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookClass>()
                .HasMany(b => b.Transactions)
                .WithOne(t => t.Book)
                .HasForeignKey(t => t.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LibraryStaffClass>()
                .HasMany(s => s.ProcessedTransactions)
                .WithOne(t => t.LibraryStaff)
                .HasForeignKey(t => t.StaffId)
                .OnDelete(DeleteBehavior.Restrict);
        }



    }
}
