using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ParallelDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            // simple Parallel.For ------------------------------------

            // conventional for
            int sum = 0;
            for (int i = 0; i < 101; i++)
            {
                sum += i;
            }

            // corresponding Parallel.For
            int sumPar = 0;
            Parallel.For(0, 101, i => Interlocked.Add(ref sumPar, i)); // add to sumPar must be done locked

            Console.WriteLine("Conventional for and Parallel.For");
            Console.WriteLine(sum + " " + sumPar);
            Console.WriteLine("========================================");
            Console.WriteLine();

            // simple Parallel.ForEach
            int[] numbers = Enumerable.Range(0, 20).ToArray();

            // conventional foreach
            int sum2 = 0;
            foreach (int n in numbers)
            {
                sum2 += n;
            }

            // corresponding Parallel.For
            int sum2Par = 0;
            Parallel.ForEach(numbers, n => Interlocked.Add(ref sum2Par, n)); // add to sumPar must be done locked

            Console.WriteLine("Conventional foreach and Parallel.Foreach");
            Console.WriteLine(sum2 + " " + sum2Par);
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.WriteLine("Parallel.For order of execution");

            // digging into threads of Parallel.For
            Parallel.For(1, 101,
              i => { Console.WriteLine("i=" + i + " ThreadId=" + Thread.CurrentThread.ManagedThreadId); });

            Console.WriteLine("========================================");
            Console.WriteLine();

            // another Foreach example

            object lockObj = new object();

            List<string> col = new List<string>();
            col.Add("Horsens");
            col.Add("Ry");
            col.Add("Aarhus");

            int min = int.MaxValue;
            int max = int.MinValue;

            ParallelLoopResult res = Parallel.ForEach<string>(col,
                s =>
                {
                    lock (lockObj) { if (s.Length < min) min = s.Length; }
                    lock (lockObj) { if (s.Length > max) max = s.Length; }
                }
            );

            Console.WriteLine("Min and max length of city names");
            Console.WriteLine(min + " " + max);
            Console.WriteLine("========================================");
            Console.WriteLine();


            // advanced Parallel.For ---------------------------------

            Console.WriteLine("Advanced Parallel.For, order of execution");

            // find sum of array
            int[] nums = Enumerable.Range(0, 20).ToArray();
            long total = 0;

            // Use type parameter to make subtotal a long, not an int
            Parallel.For<long>(0, nums.Length, () => 0,
                (j, loopState, subtotal) =>
                {
                    subtotal += nums[j];
                    lock (lockObj) Console.WriteLine("loopId=" + loopState.GetHashCode() + "   j=" + j + "   subtotal=" + subtotal + "   total=" + total);
                    return subtotal;
                },
                x =>
                {
                    lock (lockObj)
                    {
                        Console.Write("Før: " + total);
                        total += x;
                        Console.WriteLine(" - Efter: " + total);
                    }
                }
            );

            Console.WriteLine("Efter loop: total=" + total);
            Console.WriteLine("========================================");


            Console.ReadLine();
        }

    }
}
