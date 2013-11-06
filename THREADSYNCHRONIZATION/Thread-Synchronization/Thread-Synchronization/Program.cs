using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Thread_Synchronization
{
    class Program
    {
        static void Main(string[] args)
        {

            Worker worker = new Worker();
            Thread t1 = new Thread(new ThreadStart(worker.work)); t1.Name = "Th1";
            Thread t2 = new Thread(new ThreadStart(worker.work)); t2.Name = "Th2";
            Thread t3 = new Thread(new ThreadStart(worker.work)); t3.Name = "Th3";
            t1.Start();
            t2.Start();
            t3.Start();
            Console.ReadLine();
        }

    }
}
