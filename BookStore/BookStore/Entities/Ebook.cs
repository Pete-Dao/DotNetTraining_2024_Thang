using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities
{
    public class Ebook : Book
    {
        private double Filesize {  get; set; }

        public Ebook(int id, string title, string author, decimal price, int publicationYear, int quantityInStock, double filesize) : base(id, title, author, price, publicationYear, quantityInStock)
        {
            Filesize = filesize;
        }

        public override string ToString()
        {
            return base.ToString() + $", File Size: {Filesize} MB";
        }
    }
}
