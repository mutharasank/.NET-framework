using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFindAll_LambdaExpression
{
    public class Book
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public DateTime Published { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Create and fill a list of books
            List<Book> books = new List<Book> { 
new Book { Author="McConnell",Name="Code Complete", 
Published=new DateTime(1993,05,14) },
new Book { Author="Sussman",Name="SICP (2nd)", 
Published=new DateTime(1996,06,01) },
new Book { Author="Hunt",Name="Pragmatic Programmer", 
Published=new DateTime(1999,10,30) } };
            List<Book> after1995 = books.FindAll(b => b.Published.Year > 1995);
            List<Book> between1991and1997 = books.FindAll(b => b.Published.Year > 1991 && b.Published.Year < 1997);

            foreach (Book b in after1995)
            {
                Console.WriteLine(b.Name + " "+ "this book is published after 1995");
            }
            foreach (Book b in between1991and1997)
            {
                Console.WriteLine(b.Name +" "+ "Published between 1991-1997");
            }
            Console.ReadLine();
        }
    }
}
