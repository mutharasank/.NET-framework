using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ext;

namespace extensionmethods
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Once Upon a Time in the West";

            Console.WriteLine(s);
            Console.WriteLine();

            int n = s.WordCount();
            //int n = StrExt.WordCount(s);
            Console.WriteLine("Number of words: " + n);

            string init = s.Initials();
            Console.WriteLine("Initials: " + init);

            int m = 3;
            try
            {
                Console.WriteLine("Word "+ m + ": " + s.ExtractWord(m));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }

}
