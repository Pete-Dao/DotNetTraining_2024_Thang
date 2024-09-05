using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities
{
    public class Libary
    {
        private List<Book> books;

        public Libary()
        {
            books = new List<Book>();
        }

        // Add a book to the inventory
        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Adding book: \"{book.Title}\", Author: \"{book.Author}\", Price: {book.Price:C}, Year: {book.PublicationYear}, Stock: {book.QuantityInStock}");
        }

        // Remove a book by its ID
        public void RemoveBookByID(int id)
        {
            var bookToRemove = books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
                Console.WriteLine($"Book with ID {id} removed from the library.");
            }
            else
            {
                Console.WriteLine($"Book with ID {id} not found.");
            }
        }

        // Display all books in the inventory
        public void DisplayBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books in the library.");
                return;
            }

            Console.WriteLine("Books in the library:");
            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        // Get books by a specific author
        public List<Book> GetBooksByAuthor(string author)
        {
            var booksByAuthor = books.Where(b => b.Author == author).ToList();
            if (booksByAuthor.Count == 0)
            {
                Console.WriteLine($"No books found by author {author}.");
            }
            return booksByAuthor;
        }

        // Check stock of all books
        public void CheckStock()
        {
            foreach (var book in books)
            {
                if (book.QuantityInStock == 0)
                {
                    Console.WriteLine($"Out of Stock: \"{book.Title}\"");
                }
                else if (book.QuantityInStock < 5)
                {
                    Console.WriteLine($"Low Stock: \"{book.Title}\"");
                }
            }
        }

        // Get top N most expensive books using LINQ
        public List<Book> GetMostExpensiveBooks(int topN)
        {
            return books.OrderByDescending(b => b.Price).Take(topN).ToList();
        }

        // Get count of EBooks using lambda expression
        public int GetEBookCount()
        {
            return books.OfType<Ebook>().Count();
        }

        // Group books by publication year and count how many were published each year
        public void GroupBooksByPublicationYear()
        {
            var groupedBooks = books.GroupBy(b => b.PublicationYear)
                                    .Select(group => new { Year = group.Key, Count = group.Count() })
                                    .ToList();

            foreach (var group in groupedBooks)
            {
                Console.WriteLine($"Year: {group.Year}, Number of Books: {group.Count}");
            }
        }

        // Update book price by its ID
        public void UpdateBookPrice(int id, decimal newPrice)
        {
            var bookToUpdate = books.FirstOrDefault(b => b.Id == id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Price = newPrice;
                Console.WriteLine($"Price of book with ID {id} updated to {newPrice:C}.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // Generate a library report
        public void GenerateReport()
        {
            Console.WriteLine("Library Report:");
            Console.WriteLine($"Total number of books: {books.Count}");

            decimal totalInventoryValue = books.Sum(b => b.Price * b.QuantityInStock);
            Console.WriteLine($"Total inventory value: {totalInventoryValue:C}");

            var sortedBooks = books.OrderByDescending(b => b.Price).ToList();
            Console.WriteLine("Books sorted by price (descending):");
            foreach (var book in sortedBooks)
            {
                Console.WriteLine($"\"{book.Title}\", Price: {book.Price:C}");
            }

            int booksPublishedAfter2000 = books.Count(b => b.PublicationYear > 2000);
            Console.WriteLine($"Number of books published after the year 2000: {booksPublishedAfter2000}");
        }
    }
}
