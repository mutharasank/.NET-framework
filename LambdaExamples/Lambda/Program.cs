using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lambda
{
    // Documentation:
    // delegate bool Predicate<T>(T obj).
    // List<T> List<T>.FindAll(Predicate<T> match) {...}.
    // Method FindAll() returns a new list with the elements
    // that satisfies the method inside the match delegate.

    class Program
    {
        static bool IsEven(int e)
        {
            bool even = e % 2 == 0;
            return even;
        }


        static void Main(string[] args)
        {
            List<int> list = new List<int>() { 20, 1, 4, 8, 9, 44 };
            Console.WriteLine("list: " + Print<int>(list));
            Console.WriteLine();

            //-----------------------------------------------------------------

            //With defined method.

            Predicate<int> match1 = new Predicate<int>(IsEven);
            List<int> evens1 = list.FindAll(match1);
            Console.WriteLine("1: " + Print(evens1));

            Predicate<int> match2 = IsEven;
            List<int> evens2 = list.FindAll(match2);
            Console.WriteLine("2: " + Print(evens2));

            List<int> evens3 = list.FindAll(IsEven);
            Console.WriteLine("3: " + Print(evens3));

            //-----------------------------------------------------------------

            //With anonymous method.

            List<int> evens4 = list.FindAll(
                delegate(int e)
                {
                    bool even = e % 2 == 0;
                    return even;
                });
            Console.WriteLine("4: " + Print(evens4));

            //-----------------------------------------------------------------

            //With lambda method.

            List<int> evens5 = list.FindAll(
                (int e) =>
                {
                    bool even = e % 2 == 0;
                    return even;
                });
            Console.WriteLine("5: " + Print(evens5));

            //-----------------------------------------------------------------
            //With lambda method and shorter notation.

            //calculation done in one line
            List<int> evens6 = list.FindAll(
                (int e) =>
                {
                    return e % 2 == 0;
                });
            Console.WriteLine("6: " + Print(evens6));

            //formatted in one line
            List<int> evens7 = list.FindAll((int e) => { return e % 2 == 0; });
            Console.WriteLine("7: " + Print(evens7));

            //without type on parameters
            List<int> evens8 = list.FindAll((e) => { return e % 2 == 0; });
            Console.WriteLine("8: " + Print(evens8));

            //without () on parameters (allowed with only one parameter),
            //and without {} and return on code (allowed when code is only one expression)
            List<int> evens9 = list.FindAll(e => e % 2 == 0);
            Console.WriteLine("9: " + Print(evens9));

            Console.ReadLine();
        }

        //Guess the code of FindAll: 
        //public List<T> FindAll<T>(List<T> list, Predicate<T> match)
        //{
        //    //ToDo
        //}

        static string Print<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder(""); ;
            if (list.Count == 0)
                sb.Append("[]");
            else
            {
                sb.Append("[" + list[0]);
                for (int i = 1; i < list.Count; i++)
                {
                    sb.Append("," + list[i]);
                }
                sb.Append("]");
            }
            return sb.ToString();
        }
    }
}
