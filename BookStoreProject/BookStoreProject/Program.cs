using BookStoreProject.Entities;

namespace BookStoreProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            // Add books to the library
            library.AddBook(new Book(1, "C# Programming", "John Doe", 29.99m, 2018, 10));
            library.AddBook(new Book(2, "Effective LINQ", "Jane Smith", 39.99m, 2015, 4));
            library.AddBook(new Book(3, "Mastering C#", "John Doe", 49.99m, 2021, 0));

            Console.WriteLine();

            // Check stock
            library.CheckStock();

            Console.WriteLine();

            // Display top 2 most expensive books
            var mostExpensiveBooks = library.GetMostExpensiveBooks(2);
            Console.WriteLine("Most Expensive Books:");
            foreach (var book in mostExpensiveBooks)
            {
                Console.WriteLine(book.ToString());
            }

            Console.WriteLine();

            // Update book price
            library.UpdateBookPrice(1, 35.99m);

            Console.WriteLine();

            // Generate report
            library.GenerateReport();
        }
    }
}
