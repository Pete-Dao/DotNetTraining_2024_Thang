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
            library.AddBook(new EBook(2, "Effective LINQ", "Jane Smith", 39.99m, 2015, 4, 2.5));
            library.AddBook(new Book(3, "Mastering Java#", "Jon De", 19.99m, 2021, 0));
            library.AddBook(new EBook(4, "Mastering C/C++#", "Alice", 49.99m, 2021, 6, 5.6));
            library.AddBook(new Book(5, "Mastering C#", "Max", 79.99m, 2081, 7));
            library.AddBook(new Book(6, "Mastering OOP", "Sakura", 89.99m, 2016, 9));
            library.AddBook(new Book(7, "C# Programming", " John Doe ", 29.99m, 2018, 10));

            Console.WriteLine(library.books.Count());

            Console.WriteLine();
            
            // Show the library
            library.DisplayBooks();

            Console.WriteLine();    

            // Check stock
            library.CheckStock();

            Console.WriteLine();

            var bookByAuthor = library.GetBooksbyAuthor(" john doe ");
            foreach ( var book in bookByAuthor)
            {
                Console.WriteLine(book.DisplayDetail());
            }
            //Check EBook
            Console.WriteLine($"The number of EBook in the library: {library.GetEbookCount()}");

            Console.WriteLine();

            //Check group by year 
            Console.WriteLine("The report, showing how many books were published in each year: ");
            library.GroupBooksByPublicationYear();

            Console.WriteLine();

            // Display top 2 most expensive books
            var mostExpensiveBooks = library.GetMostExpensiveBooks(2);
            Console.WriteLine("Most Expensive Books:");
            foreach (var book in mostExpensiveBooks)
            {
                Console.WriteLine(book.DisplayDetail());
            }

            Console.WriteLine();

            // Update book price
            library.UpdateBookPrice(1, 35.99m);

            Console.WriteLine();
            library.DisplayBooks();

            Console.WriteLine();

            // Generate report
            library.GenerateReport();
        }
    }
}
