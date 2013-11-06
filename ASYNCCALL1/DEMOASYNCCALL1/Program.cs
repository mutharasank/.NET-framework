using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace demoasynccall1
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
            AsyncResult asyncResult = (AsyncResult) caller.BeginInvoke(6000, out threadId, null, null);

            // A sychronous call would be:   caller.Invoke(3000, out threadId);
            // or simply:                    caller(3000, out threadId);
            // but that would not give any concurrent execution
            
            Console.WriteLine("Main does some work.");
            Thread.Sleep(1800);          
            Console.WriteLine("Main does some more work.");
            Thread.Sleep(900);
            Console.WriteLine("Main waits for TestMethod to finish.");
            Console.WriteLine();
            
            //2: Call EndInvoke to wait for the asynchronous call to complete.
            string returnValue = caller.EndInvoke(out threadId, asyncResult);

            Console.WriteLine("TestMethod was executed on thread {0}.",threadId);
            Console.WriteLine("Value returned from TestMethod: {0}.", returnValue);
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
            return String.Format("My call time was {0}", callDuration);
        }

    }
}
