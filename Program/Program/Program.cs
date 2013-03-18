using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program

    {
        public static double Maxim(params double[]x)
        
        {
            double s = 0;
            foreach (int n in x)
                s = x.Max(element => Math.Abs(element));
            return s;
        }
        
   

        static void Main(string[] args)
        {
            double m1 = Maxim(28.5, 17.2);
            double m2 = Maxim(4.0, 10.8, 34.25, 2.0, 23.6);
            Console.WriteLine(m1 + " " + m2);
            Console.ReadLine();
        }
    }
}
