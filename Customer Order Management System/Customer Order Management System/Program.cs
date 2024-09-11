using System;
using Customer_Order_Management_System.Entities;

namespace Customer_Order_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderManager orderManager = new OrderManager();

            var product1 = new Product(1, "Laptop", "Electronics", 1200m, 10);
            var product2 = new Product(2, "Book", "Education", 50m, 5);
            var product3 = new Product(3, "Headphones", "Electronics", 100m, 15);
            var product4 = new Product(4, "Television", "Electronics", 500m, 20);
            var product5 = new Product(5, "Wireless mouse", "Electronics", 50m, 25);
            var product6 = new Product(6, "Wireless keyboard", "Electronics", 70m, 12);
            var product7 = new Product(7, "Pen", "Education", 0m, 1);

            // Add product to the ProductDatabase to manage
            orderManager.AddProduct(product1);
            orderManager.AddProduct(product2);
            orderManager.AddProduct(product3);
            orderManager.AddProduct(product4);
            orderManager.AddProduct(product5);
            orderManager.AddProduct(product6);
            orderManager.AddProduct(product7);

            Console.WriteLine();

            var customer1 = new Customer(101, "Alice", "alice@example.com", true);
            var customer2 = new Customer(102, "Bob", "bob@example.com", false);
            var customer3 = new Customer(103, "Pink", "pink@example.com", true);
            var customer4 = new Customer(113, "Max", "max@example.com", false);
            var customer5 = new Customer(114, "Ben", "ben@example.com", true);

            //Add customer to the CustomerDatabase to manage
            orderManager.AddCustomer(customer1);
            orderManager.AddCustomer(customer2);
            orderManager.AddCustomer(customer3);
            orderManager.AddCustomer(customer4);
            orderManager.AddCustomer(customer5);


            var order1 = new Order(1, customer1.ID, DateTime.Parse("2024-09-01"));
            order1.AddProduct(product1);
            order1.AddProduct(product4);
            order1.AddProduct(product4);
            order1.AddProduct(product6);
            order1.AddProduct(product2);
            order1.AddProduct(product3);
            order1.AddProduct(product3);
            order1.CalculateTotalAmount();

            // Choose order for customer
            customer1.AddOrder(order1);

            var order2 = new Order(2, customer2.ID, DateTime.Parse("2003-12-03"));
            order2.AddProduct(product2);
            order2.AddProduct(product3);
            order2.AddProduct(product3);
            order2.AddProduct(product4);
            order2.AddProduct(product6);
            order2.CalculateTotalAmount();

            // Choose order for customer
            customer2.AddOrder(order1);
            customer2.AddOrder(order2);

            var order3 = new Order(3, customer3.ID, DateTime.Parse("2024-01-02"));
            order3.AddProduct(product1);
            order3.AddProduct(product6);
            order3.AddProduct(product4);
            order3.AddProduct(product2);
            order3.AddProduct(product3);
            order3.AddProduct(product6);
            order3.CalculateTotalAmount();

            // Add the order to the OrderManager
            orderManager.AddOrder(order1);
            orderManager.AddOrder(order2);
            orderManager.AddOrder(order3);

            // Choose order for customer
            customer3.AddOrder(order1);
            customer3.AddOrder(order2);
            customer3.AddOrder(order3);

            Order order4 = new Order(4, customer4.ID, DateTime.Parse("1999-02-03"));
            Order order5 = new Order(5, customer5.ID, DateTime.Parse("1888-01-27"));

            order4.AddProduct(product7);
            
            orderManager.AddOrder(order4);
            orderManager.AddOrder(order5);

            customer4.AddOrder(order4);
            customer5.AddOrder(order5);

            //Update the stock of product after buying

            Console.WriteLine("\nUpdate stock product: ");
            foreach (var order in orderManager.ProductDatabase)
            {
                Console.WriteLine(order.DisplayDetail());
            }

            Console.WriteLine();

            // Get order by Id
            var ordersForCustomer = orderManager.GetOrdersByCustomer(101);
            Console.WriteLine("\nOrders for Customer ID 101:");
            foreach (var order in ordersForCustomer)
            {
                Console.WriteLine($"OrderID: {order.OrderID}, Order Date: {order.OrderDate}, Total Amount: {order.TotalAmount:c}");
            }

            // Remove orders in the specified date range
            Console.WriteLine("\nOrders removed: ");
            DateTime startDate = DateTime.Parse("2024-01-01");
            DateTime endDate = DateTime.Parse("2024-12-31");

            //orderManager.RemoveOrdersByDate(startDate, endDate);

            Console.WriteLine() ;

            //Test no order in the range Date
            DateTime startDate1 = DateTime.Parse("2000-01-01");
            DateTime endDate1 = DateTime.Parse("2000-12-31");

            orderManager.RemoveOrdersByDate(startDate1, endDate1);

            // Print the updated list of orders in the OrderDatabase
            var updatedOrders = orderManager.OrderDatabase;
            Console.WriteLine("\nUpdated list of orders after removal:");

            foreach (var order in updatedOrders)
            {
                Console.WriteLine($"- OrderID: {order.OrderID}, CustomerID: {order.CustomerID}, OrderDate: {order.OrderDate:d}");
            }

            //Calculate total sales for the "Electronics" category:

            Console.WriteLine($"\nTotal Sales for category \'Electronics\': {orderManager.GetTotalSalesByCategory("Electronics"):C}");

            //Filtering products with price greater than $50:

            Console.WriteLine("\nFiltered Products (Price > $50):");
            var filteredProducts = orderManager.FilterProducts(p => p.Price > 50);
            foreach (var product in filteredProducts)
            {
                Console.WriteLine(product.DisplayDetail());
            }

            //Applying a 10% discount to all preferred customers:

            Console.WriteLine("\nApplying Discount for Preferred Customers:");
            orderManager.ApplyDiscounts(10m, c => c.IsPreferredCustomer);

            //Show the updated orders

            foreach (var allCustomer in orderManager.CustomerDatabase)
            {
                var findById = orderManager.GetOrdersByCustomer(allCustomer.ID);
                foreach (var item in findById)
                {
                    Console.WriteLine($"Order for CustomerID: {item.OrderID}");
                    Console.WriteLine(item.Displaydetail());
                    Console.WriteLine();
                }
            }

            //Top 5 most frequently ordered products:

            orderManager.GetTop5MostFrequentlyOrderedProducts();

            Console.WriteLine();

            //Orders with total amount greater than $400:

            Console.WriteLine("High-value orders (greater than $400)");
            var getHighValueOrders = orderManager.GetHighValueOrders(400m);
            foreach(var order in getHighValueOrders)
            {
                Console.WriteLine(order.Displaydetail());
            }

            //First order by customer whose name starts with "A":
            var findWithAName = orderManager.GetFirstOrderByCustomerStartingWithA();
            Console.WriteLine($"\nFirst order by customer whose name starts with \"A\": \n{findWithAName.Displaydetail()}");


            //Restocking products with low inventory (threshold: 5):

            Console.WriteLine("\nProducts restocked: ");
            orderManager.RestockLowInventory(10, 10);

            // Generating Sales Report

            Console.WriteLine("\nGenerating Sales Report:");
            orderManager.GenerateSalesReport();

            //Top 3 customers by total amount of orders placed:

            orderManager.PrintTop3CustomersByOrderAmount();

            Console.WriteLine() ;

            //Cleaning up invalid orders:

            orderManager.CleanUpInvalidOrders();

            Console.WriteLine();

            //Segmentation of customers by purchase frequency:

            orderManager.SegmentCustomersByPurchaseFrequency(1);
        }
    }
}
