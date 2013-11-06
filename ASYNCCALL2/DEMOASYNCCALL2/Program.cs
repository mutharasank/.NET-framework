using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace demoasynccall2
{

    public delegate string AsyncMethodDelegate(int callDuration, out int threadId);

    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Main begins on thread {0}.", Thread.CurrentThread.ManagedThreadId);

            int threadId;
            AsyncMethodDelegate caller = Program.TestMethod;

            //1: Initiate the asychronous call of TestMethod (on a thread from the thread pool).
            AsyncResult asyncResult = (AsyncResult) caller.BeginInvoke(2000, out threadId, null, null);

            int i = 1;
            while (!asyncResult.IsCompleted) // loop as long as thread with Testmethod() is running
            {
                if (i % 30000000 == 0)
                {
                    Console.WriteLine("Main counting. Reached " + i);
                }
                i++;
            }
            Console.WriteLine("Main stopped counting.");
            Console.WriteLine();

            //2: Call EndInvoke to wait for the asynchronous call to complete.
            string returnValue = caller.EndInvoke(out threadId, asyncResult);

            Console.WriteLine("TestMethod was executed on thread {0}. ", threadId);
            Console.WriteLine("Value returned from TestMethod: {0}. ", returnValue);
            Console.WriteLine("Main ends.");

            Console.ReadLine();
        }

        // Method to execute asynchronously.
        public static string TestMethod(int callDuration, out int threadId)
        {
            Console.WriteLine("TestMethod begins.");
            Thread.Sleep(callDuration);
            threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("TestMethod ends.");
            return String.Format("Call time was {0}", callDuration);
        }
    }
}
