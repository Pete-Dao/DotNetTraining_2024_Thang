using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreProject.Entities
{
    public class PrintedBook : Book
    {
        private int Pagecount { get; set; }

        public PrintedBook(int id, string title, string author, decimal price, int publicationYear, int quantityInStock, int pagecount) : base(id, title, author, price, publicationYear, quantityInStock)
        {
            Pagecount = pagecount;
        }

        public override string ToString()
        {
            return base.ToString() + $", Page count: {Pagecount} pages";
        }
    }
}
