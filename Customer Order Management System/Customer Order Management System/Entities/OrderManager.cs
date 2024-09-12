using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Order_Management_System.Entities
{
    public class OrderManager
    {
        public List<Order> OrderDatabase { get; set; }
        public List<Product> ProductDatabase { get; set; }
        public List<Customer> CustomerDatabase { get; set; }

        public OrderManager()
        {
            OrderDatabase = new List<Order>();
            ProductDatabase = new List<Product>();
            CustomerDatabase = new List<Customer>();
        }

        public void AddProduct(Product product)
        {
            if (ProductDatabase.Any(p => p.ID == product.ID))
            {
                throw new Exception($"Duplicate Product ID: {product.ID} found!");
            }
            ProductDatabase.Add(product);
        }

        public void AddCustomer(Customer customer)
        {
            if (CustomerDatabase.Any(c => c.ID == customer.ID))
            {
                throw new Exception($"Duplicate Customer ID: {customer.ID} found!");
            }
            CustomerDatabase.Add(customer);
        }

        public void AddOrder(Order order)
        {
            if (OrderDatabase.Any(o => o.OrderID == order.OrderID))
            {
                throw new ArgumentException($"Error: Duplicate ID {order.OrderID} found!");
            }

            OrderDatabase.Add(order);
            Console.WriteLine("Added order: ");
            Console.WriteLine($"Order ID: {order.OrderID}, CustomerID: {order.CustomerID}, Order Date: {order.OrderDate}, Total Amount: {order.TotalAmount:c}");
        }

        public List<Order> GetOrdersByCustomer(int customerID)
        {
            return OrderDatabase.Where(o => o.CustomerID == customerID).ToList();
        }

        public void RemoveOrdersByDate(DateTime startDate, DateTime endDate)
        {
            var ordersToRemove = OrderDatabase.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
            foreach (var order in ordersToRemove)
            {
                OrderDatabase.Remove(order);
                Console.WriteLine($"- OrderID: {order.OrderID}, CustomerID: {order.CustomerID}, OrderDate: {order.OrderDate:d}");
            }

            if (!ordersToRemove.Any())
            {
                Console.WriteLine($"No orders were found in the date range from {startDate} to {endDate}.");
            }
        }

        public decimal GetTotalSalesByCategory(string category)
        {
            return OrderDatabase.SelectMany(o => o.Products)
                .Where(p => p.Category.Trim() == category.Trim())
                .Sum(p => p.Price);
        }

        // this method is high reusable
        public List<Product> FilterProducts(Func<Product, bool> filter)
        {
            return ProductDatabase.Where(filter).ToList();
        }

        public void ApplyDiscounts(decimal discountPercentage, Func<Customer, bool> condition)
        {
            var eligibleCustomers = CustomerDatabase.Where(condition).ToList();
            foreach (var customer in eligibleCustomers)
            {
                foreach (var order in customer.Orders)
                {
                    order.CalculateTotalAmount();
                    var discountAmount = order.TotalAmount * (discountPercentage / 100);
                    order.TotalAmount -= discountAmount;
                }
            }
        }

        public void GetTop5MostFrequentlyOrderedProducts()
        {
            var topProducts = OrderDatabase
                .SelectMany(o => o.Products)          
                .GroupBy(p => new { p.ID, p.Name })   
                .Select(g => new                     
                {
                    Product = g.First(),             
                    Count = g.Count()                
                })
                .OrderByDescending(g => g.Count)      
                .Take(5)                              
                .ToList();                            

            Console.WriteLine("Top 5 products by frequency:");
            int rank = 1;
            foreach (var item in topProducts)
            {
                Console.WriteLine($"{rank}. Product: \"{item.Product.Name}\", Ordered: {item.Count} times");
                rank++;
            }
        }


        public List<Order> GetHighValueOrders(decimal minAmount)
        {
            return OrderDatabase.Where(o => o.TotalAmount > minAmount).ToList();
        }

        public Order GetFirstOrderByCustomerStartingWithA()
        {
            return OrderDatabase
                .FirstOrDefault(o => CustomerDatabase.Any(c => c.Name.StartsWith("A")));
        }

        public void GenerateSalesReport()
        {
            var totalRevenue = OrderDatabase.Sum(o => o.TotalAmount);
           
            var topProduct = OrderDatabase.SelectMany(o => o.Products).GroupBy(p => p.ID).OrderByDescending(g=>g.Count()).FirstOrDefault();

            var prodName = ProductDatabase.FirstOrDefault(o => o.ID == topProduct?.Key)?.Name;

            var preferredCustomerOrders = CustomerDatabase
                .Where(c => c.IsPreferredCustomer)
                .SelectMany(c => c.Orders)
                .Count();

            var nonPreferredCustomerOrders = CustomerDatabase
                .Where(c => !c.IsPreferredCustomer)
                .SelectMany(c => c.Orders)
                .Count();

            Console.WriteLine($"Total Revenue: {totalRevenue:C}");
            Console.WriteLine($"Best-selling product: {prodName}, Total Sold: {topProduct?.Count()}");
            Console.WriteLine("- Customer Groups: ");
            Console.WriteLine($" * Preferred Customers Orders: {preferredCustomerOrders} orders");
            Console.WriteLine($" * Regular Customers: {nonPreferredCustomerOrders} orders");
        }

        public void PrintTop3CustomersByOrderAmount()
        {
            var topCustomers = CustomerDatabase
                .Select(c => new
                {
                    CustomerID = c.ID,
                    TotalAmount = c.Orders.Sum(o => o.TotalAmount)
                })
                .OrderByDescending(c => c.TotalAmount)
                .Take(3) 
                .ToList();

            Console.WriteLine("\nTop 3 customers by total order amount:");
            int rank = 1;
            foreach (var customer in topCustomers)
            {
                Console.WriteLine($"{rank}. Customer ID: {customer.CustomerID}, Total Amount: ${customer.TotalAmount:F2}");
                rank++;
            }
        }


        public void RestockLowInventory(int threshold, int restockQuantity)
        {
            var lowStockProducts = ProductDatabase.Where(p => p.StockQuantity < threshold).ToList();
            foreach (var product in lowStockProducts)
            {
                product.StockQuantity += restockQuantity;
                Console.WriteLine($"{product.DisplayDetail()}");
            }
        }

        public void CleanUpInvalidOrders()
        {
            var invalidOrders = OrderDatabase.Where(o => !o.Products.Any() || o.TotalAmount == 0).ToList();
            Console.WriteLine("Invalid orders removed: ");
            foreach (var order in invalidOrders)
            {
                string reason = !order.Products.Any() ? "No products in order" : "Total amount is zero";
                OrderDatabase.Remove(order);
                Console.WriteLine($"- OrderID: {order.OrderID}, CustomerID: {order.CustomerID}, Reason: {reason}");
            }
        }

        public void SegmentCustomersByPurchaseFrequency(int minOrders)
        {
            var frequentBuyers = CustomerDatabase
                .Where(c => c.Orders.Count > minOrders)
                .ToList();

            var occasionalBuyers = CustomerDatabase
                .Where(c => c.Orders.Count <= minOrders)
                .ToList();

            Console.WriteLine("Segmentation Results: ");
            Console.WriteLine($"Frequent Buyers (more than {minOrders}): ");
            foreach (var customer in frequentBuyers)
            {
                Console.WriteLine($"- Customer ID: {customer.ID}, Name: \"{customer.Name}\"");
            }

            Console.WriteLine($"Occasional Buyers ({minOrders} or fewer orders): ");
            foreach (var customer in occasionalBuyers)
            {
                Console.WriteLine($"- Customer ID: {customer.ID}, Name: \"{customer.Name}\"");
            }
        }
    }
}
