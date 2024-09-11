using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Order_Management_System.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalAmount { get; set; }

        // Constructor
        public Order(int orderId, int customerId, DateTime orderDate)
        {
            OrderID = orderId;
            CustomerID = customerId;
            OrderDate = orderDate;
            Products = new List<Product>();
            TotalAmount = 0m;
        }

        // Add a product to the order
        public void AddProduct(Product product)
        {
            Products.Add(product);
            CalculateTotalAmount();
            --product.StockQuantity;
        }

        // Calculate total amount based on products
        public void CalculateTotalAmount()
        {
            TotalAmount = Products.Sum(p => p.Price);
        }

        public string Displaydetail()
        {
            return $"- OrderID: {OrderID}, CustomerID: {CustomerID}, Order Date: {OrderDate}, Total Amount: {TotalAmount:c}";
        }
    }
}
