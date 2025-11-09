using AssimplyConnection.Context;
using AssimplyConnection.Models;

namespace AssimplyConnection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // using > work like try{LINQ operation}finally{_context.Dispose()}
            // which is close connection to db
            using var context = new AppDbContext();


            // Add sample data, cooment after first run
            //AddSampleData(context);

            // Access data by ID using LINQ (without ToList())
            Console.WriteLine("---- Accessing Data by ID using LINQ -----");

            // Get product by ID
            var productId = 1;
            var product = context.Products
                .Where(p => p.Id == productId)
                .Select(p => new { p.Id, p.Name, p.Price, CategoryName = p.Category.Name })
                .FirstOrDefault();

            Console.WriteLine($"Product with ID {productId}: {product?.Name} | {product?.Price}.OMR");

            // Get category by ID with products
            var categoryId = 1;
            var category = context.Categories
                .Where(c => c.Id == categoryId)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    Products = c.Products.Select(p => new { p.Id, p.Name, p.Price })
                })
                .FirstOrDefault();

            Console.WriteLine($"\nCategory with ID {categoryId}: {category?.Name}");
            Console.WriteLine("Products in this category:");
            foreach (var prod in category?.Products ?? Enumerable.Empty<object>())
            {
                Console.WriteLine($"  | {prod}");
            }

            // More LINQ queries without ToList()
            Console.WriteLine("\n----- Expensive Products (Price > 500) -----");
            var expensiveProducts = context.Products
                .Where(p => p.Price > 500)
                .Select(p => new { p.Id, p.Name, p.Price })
                .AsEnumerable(); // Using AsEnumerable() instead of ToList()

            foreach (var prod in expensiveProducts)
            {
                Console.WriteLine($"{prod.Id}: {prod.Name} | {prod.Price}.OMR");
            }

            Console.ReadLine();
        }




        static void AddSampleData(AppDbContext context)
        {
            // Check if data already exists
            if (!context.Categories.Any())
            {
                // Add categories
                var categories = new List<CategoryClass>
            {
                new CategoryClass { Name = "Electronics" },
                new CategoryClass { Name = "Books" },
                new CategoryClass { Name = "Clothing" }
            };
                context.Categories.AddRange(categories);
                context.SaveChanges();

                // Add products
                var products = new List<ProductClass>
            {
                new ProductClass { Name = "Laptop", Price = 999.99m, CategoryId = 1 },
                new ProductClass { Name = "Smartphone", Price = 699.99m, CategoryId = 1 },
                new ProductClass { Name = "Programming Book", Price = 49.99m, CategoryId = 2 },
                new ProductClass { Name = "T-Shirt", Price = 19.99m, CategoryId = 3 },
                new ProductClass { Name = "Tablet", Price = 299.99m, CategoryId = 1 }
            };
                context.Products.AddRange(products);
                context.SaveChanges();

                Console.WriteLine("Sample data added successfully!");
            }
        }
    }
}
