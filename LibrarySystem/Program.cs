using LibrarySystem.Context;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;  // <------ Don't forget this

namespace LibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext _context = new AppDbContext();
            SampleData(_context);
            Queries(_context);
        }


        static void SampleData(AppDbContext context)
        {
            // Add sample branches
            var mainBranch = new LibraryBranchClass { BranchName = "Central Library", Address = "500-Adress", Phone = "999999" };
            var northBranch = new LibraryBranchClass { BranchName = "North Branch", Address = "400-Address", Phone = "888888" };
            context.LibraryBranches.AddRange(mainBranch, northBranch);
            context.SaveChanges();


            // Add sample staff
            var staff1 = new LibraryStaffClass { FirstName = "Staff 1", Position = "Librarian", StaffBranchId = mainBranch.BranchId };
            var staff2 = new LibraryStaffClass { FirstName = "Staff 2", Position = "Assistant Librarian", StaffBranchId = mainBranch.BranchId };
            context.LibraryStaff.AddRange(staff1, staff2);
            context.SaveChanges();


            // Add sample members
            var member1 = new LibraryMemberClass { FirstName = "Member 1", Address = "500-Adress", LibraryBranchId = mainBranch.BranchId };
            var member2 = new LibraryMemberClass { FirstName = "Member 2", Address = "500-Adress", LibraryBranchId = mainBranch.BranchId };
            context.LibraryMembers.AddRange(member1, member2);
            context.SaveChanges();


            // Add sample publishers
            var publisher1 = new PublisherClass { PublisherName = "Publisher 1", Phone = "00000" };
            var publisher2 = new PublisherClass { PublisherName = "Publisher 2", Phone = "000000" };
            context.Publishers.AddRange(publisher1, publisher2);
            context.SaveChanges();


            // Add sample Category
            var fiction = new CategoryClass { CategoryName = "Fiction" };
            var scifi = new CategoryClass { CategoryName = "Science Fiction" };
            context.Categories.AddRange(fiction, scifi);
            context.SaveChanges();


            // Add sample authors
            var author1 = new AuthorClass { FirstName = "Author 1" };
            var author2 = new AuthorClass { FirstName = "Author 2" };
            context.Authors.AddRange(author1, author2);
            context.SaveChanges();


            // Add sample books
            var book1 = new BookClass { Title = "Book 1", ISBN = "0000001", PublicationYear = 2025,
                PublisherId = publisher1.PublisherId, 
                LibraryBranchId = mainBranch.BranchId, AvailabilityStatus = "Available" };
            var book2 = new BookClass { Title = "Book 2", ISBN = "0000002", PublicationYear = 2025,
                PublisherId = publisher2.PublisherId, 
                LibraryBranchId = northBranch.BranchId, AvailabilityStatus = "Checked Out" };
            context.Books.AddRange(book1, book2);
            context.SaveChanges();


            // Create book-author relationships
            var bookAuthors = new List<BookAuthorClass>
            {
                new BookAuthorClass { BookId = book1.BookId, AuthorId = author1.AuthorId }, 
                new BookAuthorClass { BookId = book2.BookId, AuthorId = author2.AuthorId }  
            };

            context.BookAuthors.AddRange(bookAuthors);
            context.SaveChanges();


            // Create book-Category relationships
            var bookCategory = new List<BookCategoryClass>
            {
                new BookCategoryClass { BookId = book1.BookId, CategoryId = fiction.GenreId },
                new BookCategoryClass { BookId = book2.BookId, CategoryId = scifi.GenreId }   
            };
            context.BookCategories.AddRange(bookCategory);
            context.SaveChanges();


            // Add sample transactions
            var transactions = new List<TransactionClass>
            {
                new TransactionClass
                {
                    BookId = book1.BookId,
                    MemberId = member1.MemberId,
                    StaffId = staff1.StaffId,
                    CheckoutDate = DateTime.Now.AddDays(-5),
                    DueDate = DateTime.Now.AddDays(9),
                    ReturnDate = null,
                    FineAmount = 0
                },
                new TransactionClass
                {
                    BookId = book2.BookId,
                    MemberId = member2.MemberId,
                    StaffId = staff2.StaffId,
                    CheckoutDate = DateTime.Now.AddDays(-10),
                    DueDate = DateTime.Now.AddDays(-1),
                    ReturnDate = null,
                    FineAmount = 3.50m
                }
            };
                context.Transactions.AddRange(transactions);
                context.SaveChanges();
        }



        static void Queries(AppDbContext context)
        {
            Console.WriteLine("\n--------------- LIBRARY SYSTEM OVERVIEW ------------");

            // Query all branches with their staff count
            var branches = context.LibraryBranches.Include(b => b.StaffMembers).ToList();

            foreach (var branch in branches)
            {
                int staffCount = 0;
                if (branch.StaffMembers != null)
                    staffCount = branch.StaffMembers.Count;
                Console.WriteLine($"Branch: {branch.BranchName}  Staff Count: {staffCount}");
            }


            Console.WriteLine("\n------------ CURRENT TRANSACTIONS ------------");
            var currentTransactions = context.Transactions
                .Include(t => t.Book)
                .Include(t => t.LibraryMember)
                .Include(t => t.LibraryStaff)
                .Where(t => t.ReturnDate == null)
                .ToList();
            foreach (var trans in currentTransactions)
            {
                string status = trans.DueDate < DateTime.Now ? "OVERDUE" : "On Time";
                Console.WriteLine($"{trans.Book.Title} | borrowed: {trans.LibraryMember.FirstName}");
                Console.WriteLine($"Due: {trans.DueDate:yyyy-MM-dd} | Status: {status} | Fine: {trans.FineAmount}.OMR");
            }


            Console.WriteLine("\n------------ ALL BOOKS WITH AUTHORS & Category ------------");
            var allBooks = context.Books
                .Include(b => b.BookCategories)
                .ThenInclude(bg => bg.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.Publisher)
                .ToList();
            foreach (var book in allBooks)
            {
                var authors = string.Join(", ", book.BookAuthors.Select(ba => $"{ba.Author.FirstName}"));
                var category = string.Join(", ", book.BookCategories.Select(bg => bg.Category.CategoryName));
                Console.WriteLine($"\n{book.Title} ({book.PublicationYear})");
                Console.WriteLine($"By: {authors}");
                Console.WriteLine($"Category: {category}");
                Console.WriteLine($"Status: {book.AvailabilityStatus} | Publisher: {book.Publisher.PublisherName}");
            }
        }

    }
}
