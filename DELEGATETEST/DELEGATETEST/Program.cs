using System;
using System.Collections.Generic;
using System.Text;

namespace DelegateTest
{
    class Test1
    {
        private int i;

        public Test1(int i) { this.i = i; }

        public void M1(string s) { Console.WriteLine("m1 - " + s + " " + i); }
    }

    class Test2
    {
        private int i;

        public Test2(int i) { this.i = i; }

        public void M2(string s) { Console.WriteLine("m2 - " + s + " " + i); }
    }

    class Test3
    {
        public static void M3(string s) { Console.WriteLine("Static method - " + s); }
    }

    class Program
    {
        public delegate void Method(string s);

        static void Main(string[] args)
        {
            Test1 t1 = new Test1(1);
            Test2 t2 = new Test2(2);

            Method method = t1.M1;
            method += t2.M2;
            method += Test3.M3;
            method += delegate(string s) { Console.WriteLine("Anonymous method - " + s); };

            method("Test");

            Console.ReadLine();
        }
    }
}
