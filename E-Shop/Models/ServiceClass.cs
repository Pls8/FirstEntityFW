using E_Shop.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class ServiceClass
    {
        //____________________Note__________________________________________________
        //Field initializer> private EShopContext _context = new EShopContext();    |
        //_context is created as soon as the object instance of EShopService        |
        //is being constructed, even before the constructor body executes.          |
        //You can’t easily pass parameters or handle logic before initialization.   |
        //--------------------------------------------------------------------------


        private AppDbContext _context;
        public ServiceClass()   // control when and how _context is created.
        {
            _context = new AppDbContext();
        }


        // Customer Methods
        public CustomerClass CreateCustomer(string name, string email, string phone)
        {
            //object initializer
            var customer = new CustomerClass
            {
                Name = name,    //something like this customer.Name = name;
                Email = email,
                Phone = phone
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        #region customer by id
        //public CustomerClass GetCustomer(int customerId)
        //{
        //    foreach (var c in _context.Customers)
        //    {
        //        if (c.CustomerId == customerId)
        //        {
        //            return c;
        //        }
        //    }
        //    return null;
        //} 
        #endregion




        // Product Methods
        public ProductsClass CreateProduct(string name, string description, decimal price, int stockQuantity, int categoryId)
        {
            var product = new ProductsClass
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = stockQuantity,
                CategoryId = categoryId
            };            


            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }


        public List<ProductsClass> GetAllProducts()
        {
            //Include is in Entity Framework(EF)
            return _context.Products.Include(p => p.Category).ToList();            
        }


        // Category Methods
        public CategoryClass CreateCategory(string name, string description)
        {
            var category = new CategoryClass
            {
                CategoryName = name,
                Description = description
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }



        // Shipping Address Methods
        public ShippingAddressClass AddShippingAddress(int customerId, string street, 
            string city, string state, string country, string postalCode)
        {
            var address = new ShippingAddressClass
            {
                CustomerId = customerId,
                StreetAddress = street,
                City = city,
                State = state,
                Country = country,
                PostalCode = postalCode
            };

            _context.ShippingAddresses.Add(address);
            _context.SaveChanges();
            return address;
        }


        // Payment Methods
        public PaymentInfoClass AddPaymentMethod(int customerId, string paymentMethod,
            string cardNumber, string expDate, string cvv)
        {
            var paymentInfo = new PaymentInfoClass
            {
                CustomerId = customerId,
                PaymentMethod = paymentMethod,
                CardNumber = cardNumber,
                ExpirationDate = expDate,
                CVV = cvv
            };

            _context.PaymentInfos.Add(paymentInfo);
            _context.SaveChanges();
            return paymentInfo;
        }





        // Order Methods
        public OrderClass CreateOrder(int customerId, int shippingAddressId,
            int paymentInfoId, List<ProductsClass> products)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var order = new OrderClass
                {
                    CustomerId = customerId,
                    OrderDate = DateTime.Now,
                    Status = "Confirmed"
                };

                decimal totalPrice = 0;

                foreach (var productOrder in products)
                {
                    var product = _context.Products.Find(productOrder.ProductId);
                    if (product == null || product.StockQuantity < productOrder.StockQuantity)
                    {
                        throw new Exception($"Product {productOrder.ProductId} not available in sufficient quantity");
                    }

                    var orderItem = new OrderItemClass
                    {
                        ProductId = productOrder.ProductId,
                        Quantity = productOrder.StockQuantity,
                        ItemPrice = product.Price
                    };

                    order.OrderItems.Add(orderItem);
                    totalPrice += product.Price * productOrder.StockQuantity;

                    // Update stock
                    product.StockQuantity -= productOrder.StockQuantity;
                }

                order.TotalPrice = totalPrice;
                _context.Orders.Add(order);
                _context.SaveChanges();
                transaction.Commit();

                return order;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }



        // Review Methods
        public ReviewClass CreateReview(int customerId, int productId, int rating, string comments)
        {
            // Check if customer purchased the product
            var hasPurchased = _context.OrderItems
                .Include(oi => oi.Order)
                .Any(oi => oi.ProductId == productId && oi.Order.CustomerId == customerId);

            if (!hasPurchased)
                throw new Exception("You can only review products you've purchased");

            var review = new ReviewClass
            {
                CustomerId = customerId,
                ProductId = productId,
                Rating = rating,
                Comments = comments
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }




        // Display Customer Methods
        public void DisplayCustomerOrders(int customerId)
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems) //Include tells EF Core to load related entities. Order > OrderItems > Product
                .ThenInclude(oi => oi.Product) //ThenInclude is used after Include to load nested related entities
                .Where(o => o.CustomerId == customerId) //sql WHERE CustomerId = @customerId
                .ToList();

            Console.WriteLine($"\nOrders for Customer {customerId}:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}, Total: {order.TotalPrice}.OMR");
                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine($" {item.Product.Name} | {item.Quantity} | {item.ItemPrice}.OMR");
                }
            }
        }


        //Disaply Product Reviews
        public void DisplayProductReviews(int productId)
        {
            var reviews = _context.Reviews
                .Include(r => r.Customer)
                .Where(r => r.ProductId == productId)
                .ToList();

            Console.WriteLine($"\nReviews for Product {productId}:");
            foreach (var review in reviews)
            {
                Console.WriteLine($"Rating: {review.Rating} | {review.Customer.Name}");
                Console.WriteLine($"Comment: {review.Comments}");
                Console.WriteLine($"Date: {review.ReviewDate}\n");
            }
        }












        //Dublication Exit
        //public void RemoveProduct(int productId)
        //{
        //    var product = _context.Products.Find(productId);
        //    if (product != null)
        //    {
        //        // Check if product is in any orders
        //        var hasOrders = _context.OrderItems.Any(oi => oi.ProductId == productId);
        //
        //        if (hasOrders)
        //        {
        //            Console.WriteLine($"Cannot delete product {product.Name} because it is associated with existing orders.");
        //            return;
        //        }
        //
        //        _context.Products.Remove(product);
        //        _context.SaveChanges();
        //        Console.WriteLine($"Product '{product.Name}' removed successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Product not found!");
        //    }
        //}

    }
}
