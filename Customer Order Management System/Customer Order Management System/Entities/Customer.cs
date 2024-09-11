using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Order_Management_System.Entities
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsPreferredCustomer { get; set; }
        public List<Order> Orders { get; set; }

        // Constructor
        public Customer(int id, string name, string email, bool isPreferredCustomer)
        {
            ID = id;
            Name = name;
            Email = email;
            IsPreferredCustomer = isPreferredCustomer;
            Orders = new List<Order>();
        }

        // Add an order to the customer
        public void AddOrder(Order order)
        {
            Orders.Add(order);
            
        }
        public string DisplayDetail()
        {
            return $"Customer ID: {ID}, Name: {Name}, Email: {Email}, Orders: {Orders.Count()} ";
        }
    }
 }
