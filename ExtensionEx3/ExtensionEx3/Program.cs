using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionEx3
{
    public static class MyMathExtension
    {
        public static int factorial(this int x)
        {
            if (x <= 1) return 1;
            else
                return x * factorial(x - 1);
        }

        public static String FixedDigits(this int x, int size)
        {

            String value = Convert.ToString(x);
            char pad = '0';
            return value.PadLeft(size, pad);
        }

        public static bool isNumeric(this string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
              
                if ((s[i] <= '0') || (s[i] > '9'))
                 
                return true;
                Console.WriteLine("contains number + " );
            }
            Console.WriteLine("contains letters");
            return false;
           
        }
    }

    class Program
    {



        static void Main(string[] args)
        {
            String s = "aa23";
            bool f = s.isNumeric();

            Console.ReadLine();
        }


    }
}
