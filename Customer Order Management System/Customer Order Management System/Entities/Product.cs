using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Customer_Order_Management_System.Entities
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Constructor
        public Product(int id, string name, string category, decimal price, int stockQuantity)
        { 
            // Initialize properties
            ID = id;
            Name = name;
            Category = category;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public string DisplayDetail()
        {
            return $"- Product ID:{ID}, Name: {Name}, Category: {Category}, Price: {Price:c}, Stock: {StockQuantity}";
        }
    }
}
