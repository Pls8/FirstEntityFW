using E_Shop.Context;
using E_Shop.Models;

namespace E_Shop
{
    internal class Program
    {
        static ServiceClass _service = new ServiceClass();

        static void Main(string[] args)
        {
            //sampleData(); // Run only once To prevent Duplcation 
            Console.WriteLine("-----------E-Shop Management System---------------");

            int choice = 0;
            do {
                DisplayMenu();
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Customer_Address_Payment_Info();
                        break;
                    case 2:
                        DisplayProducts();
                        break;
                    case 3:
                        Order();
                        break;
                    case 4:
                        Review();
                        break;
                    case 5:
                        ViewCustomerOrders();
                        break;                    
                    case 6:
                        ViewProductReviews();
                        break;
                    case 7:
                        break;
                    default:
                        Console.WriteLine("Enter 1 to 7 !");
                        break;
                }

            } while (choice != 7);
        }



        static void DisplayMenu()
        {
            Console.WriteLine("\n1. Create Customer Account | 2. View Products | 3. Place Order |\n" +
                "4. Add Product Review | 5. View Customer Orders | 6. View Product Reviews | 7. Exit");
            Console.Write("Enter your choice: ");
        }



        static void Customer_Address_Payment_Info() {
            //create user
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Console.Write("Enter phone: ");
            string phone = Console.ReadLine();
            var customer = _service.CreateCustomer(name, email, phone);
            Console.WriteLine($"Customer created successfully, ID: {customer.CustomerId}");


            //add address
            Console.Write("Enter street address: ");
            string street = Console.ReadLine();
            Console.Write("Enter city: ");
            string city = Console.ReadLine();
            Console.Write("Enter state: ");
            string state = Console.ReadLine();
            Console.Write("Enter country: ");
            string country = Console.ReadLine();
            Console.Write("Enter postal code: ");
            string postalCode = Console.ReadLine();
            _service.AddShippingAddress(customer.CustomerId, street, city, state, country, postalCode);
            Console.WriteLine("Shipping address added!");


            // Add payment method
            Console.Write("Enter payment method: ");
            string paymentMethod = Console.ReadLine();
            Console.Write("Enter card number: ");
            string cardNumber = Console.ReadLine();
            Console.Write("Enter expiration date (MM/YYYY): ");
            string expDate = Console.ReadLine();
            Console.Write("Enter CVV: ");
            string cvv = Console.ReadLine();
            _service.AddPaymentMethod(customer.CustomerId, paymentMethod, cardNumber, expDate, cvv);
            Console.WriteLine("Payment method added!");
        }



        static void DisplayProducts() {
            var products = _service.GetAllProducts();
            Console.WriteLine("\n___________ Available Products __________");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, " +
                    $"Price: {product.Price}.OMR, Stock: {product.StockQuantity}, " +
                    $"Category: {product.Category.CategoryName}");
            }
        }



        static void Order() {
            Console.Write("Enter customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            Console.Write("Enter shipping address ID: ");
            int addressId = int.Parse(Console.ReadLine());
            Console.Write("Enter payment info ID: ");
            int paymentId = int.Parse(Console.ReadLine());

            var productsCase3 = new List<ProductsClass>();
            bool addingProducts = true;

            while (addingProducts)
            {
                Console.Write("Enter product ID (0 EXIT): ");
                int productId = int.Parse(Console.ReadLine());

                if (productId == 0) break;

                Console.Write("Enter quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                // Add to list
                productsCase3.Add(new ProductsClass { ProductId = productId, StockQuantity = quantity });
            }

            try
            {
                var order = _service.CreateOrder(customerId, addressId, paymentId, productsCase3);
                Console.WriteLine($"Order placed successfully! Order ID: {order.OrderId}, Total: {order.TotalPrice}.OMR");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing order: {ex.Message}");
            }
        }



        static void Review()
        {
            Console.Write("Enter customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            Console.Write("Enter product ID: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Enter rating (1-5): ");
            int rating = int.Parse(Console.ReadLine());
            Console.Write("Enter comments: ");
            string comments = Console.ReadLine();
            try
            {
                var review = _service.CreateReview(customerId, productId, rating, comments);
                Console.WriteLine("Review added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding review: {ex.Message}");
            }
        }


        static void ViewCustomerOrders()
        {
            Console.Write("Enter customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            _service.DisplayCustomerOrders(customerId);
        }

        static void ViewProductReviews()
        {
            Console.Write("Enter product ID: ");
            int productId = int.Parse(Console.ReadLine());
            _service.DisplayProductReviews(productId);
        }



        static void sampleData() {
            try
            {
                // Create categories
                var electronics = _service.CreateCategory("Electronics", "Electronic devices and accessories");
                var clothing = _service.CreateCategory("Clothing", "Fashion and apparel");

                // Create products
                _service.CreateProduct("Smartphone", "smartphone 025", 99.99m, 50, electronics.CategoryId);
                _service.CreateProduct("Laptop", "laptop 025", 299.99m, 25, electronics.CategoryId);
                _service.CreateProduct("T-Shirt", "Cotton t-shirt", 19.99m, 100, clothing.CategoryId);

                //Console.WriteLine("Sample data initialized successfully!");
            }
            catch
            {
            }
        }

        //Dublication EXIT
        //static void RemoveProduct()
        //{
        //    _service.GetAllProducts();
        //    Console.Write("Enter product ID to remove: ");
        //    int productId = int.Parse(Console.ReadLine());

        //    _service.RemoveProduct(productId);
        //}


    }    
}
