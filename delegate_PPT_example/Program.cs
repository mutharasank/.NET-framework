using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegate_PPT_example
{
    public delegate void StockLowDel(int prodNr, int amount);

    public class Product
    {
        public StockLowDel StockLow;

        int prodNr;
        public int ProdNr { get { return prodNr; } }

        int inStock;
        public int InStock
        {
            get { return inStock; }
            set { inStock = value; }
        }

        public Product(int prodNr)
        {
            this.prodNr = prodNr;
            this.inStock = 0;
        }

        public void toStock(int amount)
        {
            inStock += amount;
        }

        public void fromStock(int amount)
        {
            inStock -= amount;

            if (inStock < 0 && StockLow != null)
                StockLow(prodNr, inStock);
        }
    }

    public class Alarm
    {
        public void stockLowUK(int prodNr, int amount)
        {
            Console.WriteLine("Product with nr. " + prodNr + " is missing in stock: " + amount);
        }

        public void stockLowDK(int prodNr, int amount)
        {
            Console.WriteLine("Produktet med nr. " + prodNr + " mangler på lager: " + amount);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Product p = new Product(345);
            Alarm alarm = new Alarm();
            p.StockLow = new StockLowDel(alarm.stockLowUK);

            p.toStock(5);
            p.fromStock(10);
            Console.WriteLine();

            p.StockLow += new StockLowDel(alarm.stockLowDK);

            p.fromStock(3);

            Console.ReadLine();
        }
    }
}
