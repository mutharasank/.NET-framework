using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadFromPool
{
    class Program
    {
        //static ManualResetEvent runSignal;
        //A thread calling runSignal.WaitOne() will block, if the signal is not set.

        static void Main(string[] args)
        {
            //1: Create the signal in not set state.
            //runSignal = new ManualResetEvent(false);

            //bool ThreadPool.QueueUserWorkItem(WaitCallBack, Object)
            //delegate:  void WaitCallBack(Object)
            ThreadPool.QueueUserWorkItem(Program.DisplayMessage, 6);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Hello from Main");
                Thread.Sleep(1000);
            }

            //Console.WriteLine("\tWaiting for runSignal");
            //2: Wait for the signal to be set by the other thread.
            //runSignal.WaitOne();

            Console.WriteLine("\tMain finished");
            Console.ReadLine();
        }

        public static void DisplayMessage(Object count)
        {
            int number = count as int? ?? 3;
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("Hello from DisplayMessage");
                Thread.Sleep(1000); //Rule: Never sleep in thread from the thread pool in a real program!
            }
            Console.WriteLine("\tDisplayMessage finished");
            
            //3: Set the signal.
            //runSignal.Set();
        }
    }
}
