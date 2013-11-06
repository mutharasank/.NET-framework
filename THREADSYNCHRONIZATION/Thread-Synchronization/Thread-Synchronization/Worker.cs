using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Thread_Synchronization
{
    class Worker
    {
        String name, job;
        int wage;
        public Worker()
        { }
        public Worker(string name, string job, int wage)
        {
            this.name = name;
            this.job = job;
            this.wage = wage;
        }
        private static object o = typeof(Worker);
        public void work()
        {
          //  lock (o)
            Monitor.Enter(o);
            {

                Console.WriteLine("================================================================");

                for (int i = 1; i <= 40; i++)
                {
                    // udskriv en linje
                    Console.Write("Output from");
                    Console.Write(Thread.CurrentThread.Name);
                    Console.Write(": " + i);
                    Console.WriteLine();
                }
            }
            Monitor.Exit(o);
        }
    }
}
