using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DemoThreadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            Worker w1 = new Worker();
            //constructor Thread(ThreadStart) or Thread(ParamaterizedThreadStart)
            //delegate: void ThreadStart()
            //delegate: void ParaneterizedThreadStart(Object)
            Thread workThread = new Thread(w1.Work);
            //workThread.Priority = ThreadPriority.Lowest;
            //workThread.IsBackground = true;
            workThread.Name = "   Worker thread";
            workThread.Start(12); //both threads are now executing

            Worker w2 = new Worker();
            w2.Work(10); //both threads are now executing the work() method

            //workThread.Abort();

            //let current thread (Main thread) wait for workThread to finish
            //workThread.Join();    

            Console.WriteLine("Main has finished");
            Console.ReadLine();
        }
    }

    public class Worker
    {
        //int limit;

        //public Worker(int limit)
        //{
        //    this.limit = limit;
        //}

        public void Work(object count)
        {
            int limit = count as int? ?? 10;
            try
            {
                long interval = 10000000;
                Console.WriteLine(Thread.CurrentThread.Name + ": Work() starts...");
                for (long i = 0; i <= limit * interval; i++)
                {
                    if (i % interval == 0)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + ": " + i / interval);
                    }
                }
                Console.WriteLine(Thread.CurrentThread.Name + ": Work() stops");
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " aborted");
            }
        }
    }

}
