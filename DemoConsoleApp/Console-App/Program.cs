using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Console_App
{
   public class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            p.Name = "Jens";
            Console.WriteLine(p.Name);

            Debug.WriteLine("test");
            Console.ReadLine();
        }
    }
}
