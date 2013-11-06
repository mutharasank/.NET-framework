using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace DemoThreadSynchronization
{
    class Program
    {
        //count is only allowed vaues 0,1 and 2
        private static int count = 0; //critical section

        [STAThread]
        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(countUp));
            t1.Name = "tUp";
            Thread t2 = new Thread(new ThreadStart(countDown));
            t2.Name = "tDn1";
            Thread t3 = new Thread(new ThreadStart(countDown));
            t3.Name = "tDn2";

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            Console.WriteLine("Main ends.");
            Console.ReadLine();
        }

        // count up 20 times
        public static void countUp()
        {
            for (int i = 0; i < 20; i++)
            {
                inc();

                // simulate thread is working
                DateTime dt = DateTime.Now;
                TimeSpan t = DateTime.Now.Subtract(dt);
                while (t.Milliseconds < 15)
                {
                    t = DateTime.Now.Subtract(dt);
                }
            }
        }

        // count down 10 times
        public static void countDown()
        {
            for (int i = 0; i < 10; i++)
            {
                dec();

                // simulate thread is working
                DateTime dt = DateTime.Now;
                TimeSpan t = DateTime.Now.Subtract(dt);
                while (t.Milliseconds < 34)
                {
                    t = DateTime.Now.Subtract(dt);
                }
            }
        }


        private static object o = typeof(Program);

        public static void inc()
        {
            lock (o)
            {
                while (count == 2)
                {
                    Console.WriteLine("   " + Thread.CurrentThread.Name + " put to wait ...");
                    Monitor.Wait(o);
                }
                count++;

                Console.WriteLine(Thread.CurrentThread.Name + ": up to " + count);
                Monitor.PulseAll(o);
            }
        }

        public static void dec()
        {
            lock (o)
            {
                while (count == 0)
                {
                    Console.WriteLine("   " + Thread.CurrentThread.Name + " put to wait ...");
                    Monitor.Wait(o);
                }
                count--;
                Console.WriteLine(Thread.CurrentThread.Name + ": down to " + count);
                Monitor.PulseAll(o);
            }
        }
    }
}
