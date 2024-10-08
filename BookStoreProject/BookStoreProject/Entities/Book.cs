﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreProject.Entities
{
     public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int PublicationYear { get; set; }
        public int QuantityInStock { get; set; }

        public Book(int id, string title, string author, decimal price, int publicationYear, int quantityInStock)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
            PublicationYear = publicationYear;
            QuantityInStock = quantityInStock;
        }

        public virtual string DisplayDetail()
        {
            return $"ID: {Id}, Title: {Title}, Author: {Author}, Price: ${Price}, " +
                    $"Publication Year: {PublicationYear}, Stock: {QuantityInStock}";
        }
    }
}
